using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Bus;

public class BusDto
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public BusStatus Status { get; set; }
}