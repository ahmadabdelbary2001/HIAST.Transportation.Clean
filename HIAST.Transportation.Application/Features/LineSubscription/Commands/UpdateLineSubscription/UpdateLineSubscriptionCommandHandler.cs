using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineSubscription.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.UpdateLineSubscription;

public class UpdateLineSubscriptionCommandHandler : IRequestHandler<UpdateLineSubscriptionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLineSubscriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLineSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLineSubscriptionDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineSubscriptionDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var lineSubscription = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(request.LineSubscriptionDto.Id);
        if (lineSubscription == null)
            throw new NotFoundException(nameof(Domain.Entities.LineSubscription), request.LineSubscriptionDto.Id);

        _mapper.Map(request.LineSubscriptionDto, lineSubscription);
        
        await _unitOfWork.LineSubscriptionRepository.UpdateAsync(lineSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}