using ParkingApp.Core.Entities;
using ParkingApp.Core.Factories;
using ParkingApp.Core.Settings;
using ParkingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ParkingApp.Web.Services;

public class ReservationMediator
{
    private readonly ParkingDbContext _db;
    private readonly AccessFactory _digitalFactory = new DigitalAccessFactory();
    private readonly AccessFactory _manualFactory = new ManualAccessFactory();

    public ReservationMediator(ParkingDbContext db) => _db = db;

    // Metoda sprawdzająca zajęte miejsca na daną datę
    public async Task<List<int>> GetOccupiedSpotIdsAsync(DateTime date, ParkingDbContext db)
    {
        return await db.Reservations
            .Where(r => r.ReservedForDate.Date == date.Date && r.IsActive)
            .Select(r => r.ParkingSpotId)
            .ToListAsync();
    }

    // Zaktualizowana metoda tworzenia rezerwacji z datą
    public async Task<string> CreateReservation(int spotId, string userId, bool usePin, DateTime reservedDate)
    {
        AccessFactory factory = usePin ? _manualFactory : _digitalFactory;
        var accessData = factory.CreateMethod().GetAccessData();

        var reservation = new Reservation
        {
            UserId = userId,
            ParkingSpotId = spotId,
            AccessCode = accessData,
            ReservedForDate = reservedDate, // Zapisujemy wybraną datę
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();

        ParkingStatistics.Instance.Increment();

        return accessData;
    }

    // Pomocnicza metoda dla Interpretera (jeśli wciąż jej używasz)
    public ParkingApp.Core.Interpreter.SearchContext GetFilters(string query)
    {
        var context = new ParkingApp.Core.Interpreter.SearchContext();
        new ParkingApp.Core.Interpreter.QueryExpression(query).Interpret(context);
        return context;
    }
}