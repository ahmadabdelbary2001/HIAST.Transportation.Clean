namespace HIAST.Transportation.Application.DTOs.Line;

public class CreateLineDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? SupervisorId { get; set; }
}