using HIAST.Transportation.Application.DTOs.Bus;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.CreateBus;

public class CreateBusCommand : IRequest<int>
{
    public CreateBusDto BusDto { get; set; } = null!;
}