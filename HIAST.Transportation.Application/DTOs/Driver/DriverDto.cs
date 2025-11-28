namespace HIAST.Transportation.Application.DTOs.Driver;

public class DriverDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string? ContactInfo { get; set; }
    
    public int? LineId { get; set; }
    public string? LineName { get; set; }
    public int? BusId { get; set; }
    public string? BusLicensePlate { get; set; } // Bus on that line
    public DateTime CreatedAt { get; set; } // Driver's creation date
    public DateTime? UpdatedAt { get; set; } // Driver's last update date
}