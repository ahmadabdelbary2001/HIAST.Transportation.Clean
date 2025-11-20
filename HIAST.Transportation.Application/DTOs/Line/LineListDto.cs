namespace HIAST.Transportation.Application.DTOs.Line;

public class LineListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SupervisorName { get; set; } = string.Empty;
}