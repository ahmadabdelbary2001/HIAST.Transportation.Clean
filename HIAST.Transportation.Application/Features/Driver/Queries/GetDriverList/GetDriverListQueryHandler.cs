using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Driver;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Queries.GetDriverList;

public class GetDriverListQueryHandler : IRequestHandler<GetDriverListQuery, List<DriverListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<DriverListDto>> Handle(GetDriverListQuery request, CancellationToken cancellationToken)
    {
        var drivers = await _unitOfWork.DriverRepository.GetAllAsync();
        return _mapper.Map<List<DriverListDto>>(drivers);
    }
}