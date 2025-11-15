using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineStop;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Queries.GetLineStopDetail;

public class GetLineStopDetailQueryHandler : IRequestHandler<GetLineStopDetailQuery, LineStopDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineStopDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LineStopDto> Handle(GetLineStopDetailQuery request, CancellationToken cancellationToken)
    {
        var lineStop = await _unitOfWork.LineStopRepository.GetByIdAsync(request.Id);
        if (lineStop == null)
            throw new NotFoundException(nameof(Domain.Entities.LineStop), request.Id);

        return _mapper.Map<LineStopDto>(lineStop);
    }
}