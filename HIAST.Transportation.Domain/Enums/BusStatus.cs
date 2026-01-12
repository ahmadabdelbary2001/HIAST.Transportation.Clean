namespace HIAST.Transportation.Domain.Enums;

public enum BusStatus
{
    Available = 1,
    InService = 2,
    [Obsolete("Use OutOfService instead")]
    UnderMaintenance = 3,
    OutOfService = 4
}