namespace HIAST.Transportation.Application.DTOs.Driver;

public class CreateDriverDto
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string? ContactInfo { get; set; }
}