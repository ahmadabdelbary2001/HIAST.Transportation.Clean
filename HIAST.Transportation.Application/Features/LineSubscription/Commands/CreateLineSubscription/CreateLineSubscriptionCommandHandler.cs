using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineSubscription.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.CreateLineSubscription;

public class CreateLineSubscriptionCommandHandler : IRequestHandler<CreateLineSubscriptionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLineSubscriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLineSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLineSubscriptionDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineSubscriptionDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid LineSubscription", validationResult);

        var lineSubscription = _mapper.Map<Domain.Entities.LineSubscription>(request.LineSubscriptionDto);

        await _unitOfWork.LineSubscriptionRepository.CreateAsync(lineSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lineSubscription.Id;
    }
}