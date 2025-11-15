namespace HIAST.Transportation.Application.DTOs.LineStop;

public class UpdateLineStopDto
{
    public int Id { get; set; }
    public int LineId { get; set; }
    public int StopId { get; set; }
    public int SequenceOrder { get; set; }
}