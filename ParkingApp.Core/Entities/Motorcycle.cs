

namespace ParkingApp.Core.Entities;

public sealed class Motorcycle : Vehicle
{
    public string LicensePlate { get; set; } = string.Empty;
    public override string GetVehicleType() => "Motorcycle";
}
  