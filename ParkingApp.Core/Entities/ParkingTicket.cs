using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.Core.Entities;

// Produkt (bazowy)
public abstract class ParkingTicket
{
    public abstract string GetDetails();
}

// Konkretny Produkt 1
public class StandardTicket : ParkingTicket
{
    public override string GetDetails() => "Papierowy bilet standardowy.";
}

// Konkretny Produkt 2
public class DigitalTicket : ParkingTicket
{
    public override string GetDetails() => "Cyfrowy bilet w aplikacji.";
}