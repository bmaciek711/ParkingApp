using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.Core.Settings;

//Singleton

public class SystemConfiguration
{
    public Dictionary<string, int> VehicleLimits { get; } = new()
    {
        { "Default", 2 },
        { "VIP", 10 },
        { "Admin", 999 }
    };

}
