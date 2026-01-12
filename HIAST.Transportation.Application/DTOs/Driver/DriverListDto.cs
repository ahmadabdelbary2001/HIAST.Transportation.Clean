namespace HIAST.Transportation.Application.DTOs.Driver;

public class DriverListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public bool IsAssigned { get; set; }
}