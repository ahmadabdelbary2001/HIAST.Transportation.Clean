using HIAST.Transportation.Application.DTOs.Driver;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Queries.GetDriverList;

public class GetDriverListQuery : IRequest<List<DriverListDto>>
{
}