using ParkingApp.Core.Entities;

namespace ParkingApp.Application.PatternsFactories;

// CREATOR (Twórca) - Serce wzorca
public abstract class TicketFactory
{
    // To jest ta słynna "Metoda Fabrykująca"
    public abstract ParkingTicket CreateTicket();

    // Metoda pomocnicza wykonująca operację na produkcie
    public string GenerateTicketInfo()
    {
        var ticket = CreateTicket();
        return $"[SYSTEM] {ticket.GetDetails()} - Wygenerowano: {DateTime.Now:HH:mm}";
    }
}

// Konkretny Twórca 1
public class StandardTicketFactory : TicketFactory
{
    public override ParkingTicket CreateTicket() => new StandardTicket();
}

// Konkretny Twórca 2
public class DigitalTicketFactory : TicketFactory
{
    public override ParkingTicket CreateTicket() => new DigitalTicket();
}