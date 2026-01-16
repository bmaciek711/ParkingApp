using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Interfaces;
using ParkingApp.Infrastructure.Data;

namespace ParkingApp.Infrastructure.Repositories;

public sealed class EfRepository<T> : IRepository<T> where T : class
{
    private readonly ParkingDbContext _db;

    public EfRepository(ParkingDbContext db)
    {
        _db = db;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Set<T>().FindAsync([id], ct);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await _db.Set<T>().AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _db.Set<T>().AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }
    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync(ct);
    }


    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity is null) return;

        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
