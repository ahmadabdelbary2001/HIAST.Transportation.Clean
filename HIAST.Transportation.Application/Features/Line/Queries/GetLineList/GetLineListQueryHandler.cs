using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Line;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineList;

public class GetLineListQueryHandler : IRequestHandler<GetLineListQuery, List<LineListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LineListDto>> Handle(GetLineListQuery request, CancellationToken cancellationToken)
    {
        var lines = await _unitOfWork.LineRepository.GetAllAsync();
        return _mapper.Map<List<LineListDto>>(lines);
    }
}