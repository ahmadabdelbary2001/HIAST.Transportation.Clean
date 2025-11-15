using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Trip.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Commands.CreateTrip;

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTripCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTripDtoValidator();
        var validationResult = await validator.ValidateAsync(request.TripDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var trip = _mapper.Map<Domain.Entities.Trip>(request.TripDto);

        await _unitOfWork.TripRepository.CreateAsync(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return trip.Id;
    }
}