using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Stop;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Queries.GetStopDetail;

public class GetStopDetailQueryHandler : IRequestHandler<GetStopDetailQuery, StopDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStopDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StopDto> Handle(GetStopDetailQuery request, CancellationToken cancellationToken)
    {
        var stop = await _unitOfWork.StopRepository.GetByIdAsync(request.Id);
        if (stop == null)
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.Id);

        return _mapper.Map<StopDto>(stop);
    }
}