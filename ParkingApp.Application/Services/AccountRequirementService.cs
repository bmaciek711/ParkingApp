using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApp.Core.Interfaces;

public class AccountRequirementService
{
    public IAccountFactory GetFactory(IEnumerable<string> roles)
    {
        if (roles.Contains("Admin")) return new AdminAccountFactory();
        if (roles.Contains("VIP")) return new VipAccountFactory();
        return new DefaultAccountFactory();
    }
}