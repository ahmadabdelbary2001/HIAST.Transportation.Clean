using HIAST.Transportation.Application.DTOs.Driver;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.UpdateDriver;

public class UpdateDriverCommand : IRequest<Unit>
{
    public UpdateDriverDto DriverDto { get; set; } = null!;
}