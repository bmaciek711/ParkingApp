namespace ParkingApp.Core.Entities;

public abstract class Vehicle : BaseEntity
{
    public string OwnerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LicensePlate { get; set; }
    public abstract string GetVehicleType();
}