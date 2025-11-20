namespace HIAST.Transportation.Application.DTOs.Stop;

public class StopDto
{
    public int Id { get; set; }
    public int LineId { get; set; }
    public string Address { get; set; } = string.Empty;
    public int SequenceOrder { get; set; }
}