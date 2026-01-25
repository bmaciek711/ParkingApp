using ParkingApp.Core.Interfaces;
using ParkingApp.Core.Entities;
using ParkingApp.Application.PatternsFactories;

namespace ParkingApp.Application.Services;

public class ParkingService : IParkingService
{
    private readonly IRepository<ParkingSpot> _spotRepository;

    public ParkingService(IRepository<ParkingSpot> spotRepository)
    {
        _spotRepository = spotRepository;
    }

    public async Task<IReadOnlyList<ParkingSpot>> GetSpotsAsync(CancellationToken ct = default)
    {
        return await _spotRepository.GetAllAsync(ct);
    }

    public string IssueTicket(string ticketType)
    {
        TicketFactory factory = ticketType switch
        {
            "Standard" => new StandardTicketFactory(),
            "Digital" => new DigitalTicketFactory(),
            _ => throw new ArgumentException("Nieznany typ biletu")
        };

        return factory.GenerateTicketInfo();
    }
}