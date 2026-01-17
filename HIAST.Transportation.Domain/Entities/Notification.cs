using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Notification : AuditableEntity
{
    public required string Title { get; set; } // Fallback/legacy
    public required string Message { get; set; } // Fallback/legacy
    public string? TitleKey { get; set; } // i18n translation key for title
    public string? MessageKey { get; set; } // i18n translation key for message
    public string? Data { get; set; } // JSON data for translation interpolation
    public required string UserId { get; set; } // The recipient
    public bool IsRead { get; set; } = false;
    public string? RelatedEntityId { get; set; } // E.g., LineId
    public string? Type { get; set; } // E.g., "SupervisorChange", "Handover"
}
