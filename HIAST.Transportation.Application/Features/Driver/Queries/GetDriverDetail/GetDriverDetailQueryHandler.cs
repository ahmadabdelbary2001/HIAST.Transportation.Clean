using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Driver;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Queries.GetDriverDetail;

public class GetDriverDetailQueryHandler : IRequestHandler<GetDriverDetailQuery, DriverDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DriverDto> Handle(GetDriverDetailQuery request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(request.Id);
        if (driver == null)
            throw new NotFoundException(nameof(Domain.Entities.Driver), request.Id);

        return _mapper.Map<DriverDto>(driver);
    }
}