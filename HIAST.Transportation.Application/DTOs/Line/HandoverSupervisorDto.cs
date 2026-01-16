namespace HIAST.Transportation.Application.DTOs.Line;

public class HandoverSupervisorDto
{
    public int LineId { get; set; }
    public required string NewSupervisorId { get; set; }
}
