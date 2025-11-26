using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Stop;

public class CreateStopDto
{
    public int LineId { get; set; }
    public string Address { get; set; } = string.Empty;
    public int SequenceOrder { get; set; }
    public StopType StopType { get; set; }
}