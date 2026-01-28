namespace ParkingApp.Core.Entities;

public sealed class Reservation : BaseEntity
{
    public int ParkingSpotId { get; set; }
    public ParkingSpot ParkingSpot { get; set; } = default!;

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; } = true;

}
