using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionDetail;

public class GetLineSubscriptionDetailQueryHandler : IRequestHandler<GetLineSubscriptionDetailQuery, LineSubscriptionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineSubscriptionDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LineSubscriptionDto> Handle(GetLineSubscriptionDetailQuery request, CancellationToken cancellationToken)
    {
        var lineSubscription = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(request.Id);
        if (lineSubscription == null)
            throw new NotFoundException(nameof(Domain.Entities.LineSubscription), request.Id);

        return _mapper.Map<LineSubscriptionDto>(lineSubscription);
    }
}