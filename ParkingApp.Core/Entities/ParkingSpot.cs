namespace ParkingApp.Core.Entities;

public sealed class ParkingSpot : BaseEntity
{
    public int Number { get; set; }

    public bool IsVipOnly { get; set; }

    public bool IsUnderMaintenance { get; set; }
}
