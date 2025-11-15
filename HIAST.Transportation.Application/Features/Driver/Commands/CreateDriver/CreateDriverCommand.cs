using HIAST.Transportation.Application.DTOs.Driver;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommand : IRequest<int>
{
    public CreateDriverDto DriverDto { get; set; } = null!;
}