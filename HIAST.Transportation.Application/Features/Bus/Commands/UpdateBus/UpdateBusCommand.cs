using HIAST.Transportation.Application.DTOs.Bus;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.UpdateBus;

public class UpdateBusCommand : IRequest<Unit>
{
    public UpdateBusDto BusDto { get; set; } = null!;
}