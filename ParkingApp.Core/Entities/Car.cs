using ParkingApp.Core.Entities;

namespace ParkingApp.Core.Entities;

public sealed class Car : Vehicle
{
    public override string GetVehicleType() => "Car";
}
