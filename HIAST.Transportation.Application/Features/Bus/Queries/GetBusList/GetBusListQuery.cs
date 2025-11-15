using HIAST.Transportation.Application.DTOs.Bus;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Queries.GetBusList;

public class GetBusListQuery : IRequest<List<BusListDto>>
{
}