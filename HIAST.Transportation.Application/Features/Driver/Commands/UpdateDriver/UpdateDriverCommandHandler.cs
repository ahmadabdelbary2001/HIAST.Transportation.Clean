using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.UpdateDriver;

public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateDriverDtoValidator();
        var validationResult = await validator.ValidateAsync(request.DriverDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid Driver", validationResult);
        
        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(request.DriverDto.Id);
        if (driver == null)
            throw new NotFoundException(nameof(Domain.Entities.Driver), request.DriverDto.Id);

        _mapper.Map(request.DriverDto, driver);
        
        await _unitOfWork.DriverRepository.UpdateAsync(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}