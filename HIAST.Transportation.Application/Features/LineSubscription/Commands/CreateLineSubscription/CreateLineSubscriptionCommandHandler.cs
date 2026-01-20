using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.DTOs.LineSubscription.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.CreateLineSubscription;

public class CreateLineSubscriptionCommandHandler : IRequestHandler<CreateLineSubscriptionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateLineSubscriptionCommandHandler> _logger;
    private readonly Contracts.Infrastructure.INotificationService _notificationService;
    private readonly IUserService _userService;

    public CreateLineSubscriptionCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IAppLogger<CreateLineSubscriptionCommandHandler> logger,
        Contracts.Infrastructure.INotificationService notificationService,
        IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _notificationService = notificationService;
        _userService = userService;
    }

    public async Task<int> Handle(CreateLineSubscriptionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line subscription creation process");

        var validator = new CreateLineSubscriptionDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineSubscriptionDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Line subscription creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid LineSubscription", validationResult);
        }

        // Capacity check
        var lineWithDetails = await _unitOfWork.LineRepository.GetLineWithDetailsAsync(request.LineSubscriptionDto.LineId);
        if (lineWithDetails == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Line), request.LineSubscriptionDto.LineId);
        }

        var currentSubscriptions = await _unitOfWork.LineSubscriptionRepository.GetSubscriptionsByLineIdAsync(request.LineSubscriptionDto.LineId);
        var activeSubsCount = currentSubscriptions.Count(s => s.IsActive);

        if (activeSubsCount >= lineWithDetails.Bus.Capacity)
        {
            _logger.LogWarning("Capacity reached for line {LineId}. Bus {BusId} capacity is {Capacity}.", 
                lineWithDetails.Id, lineWithDetails.BusId, lineWithDetails.Bus.Capacity);

            // Get employee details for notification
            var employee = await _userService.GetEmployee(request.LineSubscriptionDto.EmployeeId);
            var employeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : "Unknown";

            // Notify all administrators
            var adminIds = await _userService.GetAdminUserIdsAsync();
            var notificationData = System.Text.Json.JsonSerializer.Serialize(new
            {
                employeeName = employeeName,
                lineName = lineWithDetails.Name,
                busLicensePlate = lineWithDetails.Bus.LicensePlate,
                capacity = lineWithDetails.Bus.Capacity
            });

            foreach (var adminId in adminIds)
            {
                await _notificationService.SendNotificationAsync(
                    adminId,
                    "Bus Capacity Reached",
                    $"Employee {employeeName} attempted to subscribe to line {lineWithDetails.Name}, but bus {lineWithDetails.Bus.LicensePlate} is full ({lineWithDetails.Bus.Capacity} seats)",
                    lineWithDetails.Id.ToString(),
                    "BusFull",
                    "notifications.busFull.title",
                    "notifications.busFull.message",
                    notificationData
                );
            }

            throw new BadRequestException("The bus for this line has reached its maximum capacity.");
        }

        var lineSubscription = _mapper.Map<Domain.Entities.LineSubscription>(request.LineSubscriptionDto);
        
        _logger.LogInformation("Creating line subscription for line ID: {LineId} and employee ID: {EmployeeId}", 
            lineSubscription.LineId, lineSubscription.EmployeeUserId);

        await _unitOfWork.LineSubscriptionRepository.CreateAsync(lineSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line subscription created successfully with ID: {LineSubscriptionId}", lineSubscription.Id);
        return lineSubscription.Id;
    }
}