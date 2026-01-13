namespace HIAST.Transportation.Application.DTOs.Stop;

public class ReorderStopsDto
{
    public int LineId { get; set; }
    public List<StopOrderDto> Stops { get; set; } = new();
}

public class StopOrderDto
{
    public int Id { get; set; }
    public int SequenceOrder { get; set; }
    public Domain.Enums.StopType StopType { get; set; }
}
