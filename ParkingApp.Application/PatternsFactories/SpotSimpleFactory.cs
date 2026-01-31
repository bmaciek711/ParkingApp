using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParkingApp.Core.Entities;

namespace ParkingApp.Application.PatternsFactories;

public static class SpotSimpleFactory
{
    public static ParkingSpot CreateSpot(string type, int number, bool isVip)
    {
        return type switch
        {
            "Samochód" => new ParkingSpot { Number = number, SpotType = "Samochód", IsVipOnly = isVip },
            "Motocykl" => new ParkingSpot { Number = number, SpotType = "Motocykl", IsVipOnly = isVip },
            "Rower" => new ParkingSpot { Number = number, SpotType = "Rower", IsVipOnly = false }, // Rowery nigdy nie są VIP
            _ => throw new ArgumentException("Nieznany typ miejsca")
        };
    }
}