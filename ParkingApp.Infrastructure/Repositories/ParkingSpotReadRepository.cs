using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Data;

namespace ParkingApp.Infrastructure.Repositories;

public class ParkingSpotReadRepository : IParkingSpotReadRepository
{
    private readonly ParkingDbContext _db;

    public ParkingSpotReadRepository(ParkingDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ParkingSpot>> GetAvailableAsync(
        DateTime from,
        DateTime to,
        string? searchText,
        bool isVipUser,
        CancellationToken ct)
    {
        var spots = _db.ParkingSpots.AsQueryable();

        // ❌ miejsca w trakcie konserwacji są niedostępne
        spots = spots.Where(s => !s.IsUnderMaintenance);

        // ❌ jeśli użytkownik nie jest VIP → nie pokazujemy miejsc VIP
        if (!isVipUser)
            spots = spots.Where(s => !s.IsVipOnly);

        // 🔍 wyszukiwanie po numerze miejsca (opcjonalne)
        if (!string.IsNullOrWhiteSpace(searchText)
            && int.TryParse(searchText, out var spotNumber))
        {
            spots = spots.Where(s => s.Number == spotNumber);
        }

        return await spots.ToListAsync(ct);
    }
}
