using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.Core.Settings;

//Singleton

public class ParkingStatistics
{
    private static readonly ParkingStatistics _instance = new();
    public static ParkingStatistics Instance => _instance;

    public int IssuedAccessCodes { get; private set; }
    public void Increment() => IssuedAccessCodes++;
}