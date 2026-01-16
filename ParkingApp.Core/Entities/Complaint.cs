
namespace ParkingApp.Core.Entities;

public sealed class Complaint : BaseEntity
{
    public string Description { get; set; } = string.Empty;

    // opcjonalnie – kto zgłosił
    public string ReporterId { get; set; } = string.Empty;

    // opcjonalnie – status
    public bool IsResolved { get; set; }
}
