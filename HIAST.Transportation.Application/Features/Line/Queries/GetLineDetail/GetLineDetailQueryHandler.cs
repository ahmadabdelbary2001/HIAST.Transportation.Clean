using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineDetail;

public class GetLineDetailQueryHandler : IRequestHandler<GetLineDetailQuery, LineDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LineDto> Handle(GetLineDetailQuery request, CancellationToken cancellationToken)
    {
        var line = await _unitOfWork.LineRepository.GetByIdAsync(request.Id);
        if (line == null)
            throw new NotFoundException(nameof(Domain.Entities.Line), request.Id);

        return _mapper.Map<LineDto>(line);
    }
}