using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Trip.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Commands.UpdateTrip;

public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTripCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateTripDtoValidator();
        var validationResult = await validator.ValidateAsync(request.TripDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripDto.Id);
        if (trip == null)
            throw new NotFoundException(nameof(Domain.Entities.Trip), request.TripDto.Id);

        _mapper.Map(request.TripDto, trip);
        
        await _unitOfWork.TripRepository.UpdateAsync(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}