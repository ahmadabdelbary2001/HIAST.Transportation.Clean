namespace HIAST.Transportation.Application.DTOs.Stop;

public class UpdateStopDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Address { get; set; }
}