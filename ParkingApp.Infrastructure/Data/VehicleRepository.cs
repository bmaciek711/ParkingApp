using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Data;
using ParkingApp.Core.Entities;

namespace ParkingApp.Infrastructure.Data;

public class VehicleRepository : IRepository<Vehicle>
{
    private readonly ParkingDbContext _context;

    public VehicleRepository(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Vehicles.ToListAsync(ct);
    }

    public async Task AddAsync(Vehicle entity, CancellationToken ct = default)
    {
        await _context.Vehicles.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Vehicle entity, CancellationToken ct = default)
    {
        _context.Vehicles.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Vehicle entity, CancellationToken ct = default)
    {
        _context.Vehicles.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}