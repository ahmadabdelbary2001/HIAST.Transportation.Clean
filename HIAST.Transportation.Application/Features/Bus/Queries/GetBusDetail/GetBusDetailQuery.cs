using HIAST.Transportation.Application.DTOs.Bus;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Queries.GetBusDetail;

public class GetBusDetailQuery : IRequest<BusDto>
{
    public int Id { get; set; }
}