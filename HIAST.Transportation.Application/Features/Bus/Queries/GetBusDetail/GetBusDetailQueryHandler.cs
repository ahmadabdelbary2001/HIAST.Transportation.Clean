using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Bus;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Queries.GetBusDetail;

public class GetBusDetailQueryHandler : IRequestHandler<GetBusDetailQuery, BusDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBusDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BusDto> Handle(GetBusDetailQuery request, CancellationToken cancellationToken)
    {
        var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.Id);
        if (bus == null)
            throw new NotFoundException(nameof(Domain.Entities.Bus), request.Id);

        return _mapper.Map<BusDto>(bus);
    }
}