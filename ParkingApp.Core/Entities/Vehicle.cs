namespace ParkingApp.Core.Entities;

public abstract class Vehicle : BaseEntity
{
    public string OwnerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public abstract string GetVehicleType();
}
