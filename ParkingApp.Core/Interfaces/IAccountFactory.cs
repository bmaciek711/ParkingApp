using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApp.Core.Settings;

namespace ParkingApp.Core.Interfaces;

//Abstract Factory

public interface IAccountDescription { string GetFullName(); string GetThemeColor(); }
public interface IAccountLimit { int GetLimit(SystemConfiguration config); }


public interface IAccountFactory
{
    IAccountDescription CreateDescription();
    IAccountLimit CreateLimit();
}

//  Konto Standardowe
public class StandardDescription : IAccountDescription
{
    public string GetFullName() => "Użytkownik Standardowy";
    public string GetThemeColor() => "secondary";
}
public class StandardLimit : IAccountLimit
{
    public int GetLimit(SystemConfiguration config) => config.VehicleLimits["Default"];
}

public class DefaultAccountFactory : IAccountFactory
{
    public IAccountDescription CreateDescription() => new StandardDescription();
    public IAccountLimit CreateLimit() => new StandardLimit();
}

// Konto VIP
public class VipDescription : IAccountDescription
{
    public string GetFullName() => "Użytkownik Premium VIP";
    public string GetThemeColor() => "warning text-dark";
}
public class VipLimit : IAccountLimit
{
    public int GetLimit(SystemConfiguration config) => config.VehicleLimits["VIP"];
}

public class VipAccountFactory : IAccountFactory
{
    public IAccountDescription CreateDescription() => new VipDescription();
    public IAccountLimit CreateLimit() => new VipLimit();
}

// Konto Admin
public class AdminDescription : IAccountDescription
{
    public string GetFullName() => "Główny Administrator";
    public string GetThemeColor() => "danger";
}
public class AdminLimit : IAccountLimit
{
    public int GetLimit(SystemConfiguration config) => config.VehicleLimits["Admin"];
}

public class AdminAccountFactory : IAccountFactory
{
    public IAccountDescription CreateDescription() => new AdminDescription();
    public IAccountLimit CreateLimit() => new AdminLimit();
}