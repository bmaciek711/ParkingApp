using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Entities;

public sealed class Bicycle : Vehicle
{
    public override string GetVehicleType() => "Car";
}
