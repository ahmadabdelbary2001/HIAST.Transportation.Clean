using HIAST.Transportation.Application.DTOs.Driver;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Queries.GetDriverDetail;

public class GetDriverDetailQuery : IRequest<DriverDto>
{
    public int Id { get; set; }
}