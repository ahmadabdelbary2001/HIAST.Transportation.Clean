namespace HIAST.Transportation.Application.DTOs.Line;

public class LineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? SupervisorId { get; set; }
    public string SupervisorName { get; set; } = string.Empty;
}