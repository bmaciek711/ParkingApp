using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Entities;

public sealed class Car : Vehicle
{
    public string LicensePlate { get; set; } = string.Empty;
    public override string GetVehicleType() => "Car";
}
