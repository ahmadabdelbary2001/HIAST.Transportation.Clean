namespace HIAST.Transportation.Application.DTOs.Driver.Validators;

public class DriverWithLineDetails
{
    public Domain.Entities.Driver Driver { get; set; } = null!;
    public Domain.Entities.Line? Line { get; set; }
    public Domain.Entities.Bus? Bus { get; set; }
}