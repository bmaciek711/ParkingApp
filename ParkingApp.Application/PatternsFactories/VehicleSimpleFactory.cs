using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApp.Core.Entities;

namespace ParkingApp.Application.PatternsFactories;

public static class VehicleSimpleFactory
{
    public static Vehicle Create(string type, string name, string plate)
    {
        return type switch
        {
            "Car" => new Car { Name = name, LicensePlate = plate },
            "Motorcycle" => new Motorcycle { Name = name, LicensePlate = plate },
            "Bicycle" => new Bicycle { Name = name }, 
            _ => throw new ArgumentException("Nieznany typ pojazdu")
        };
    }
}