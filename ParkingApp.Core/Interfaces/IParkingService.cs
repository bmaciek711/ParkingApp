using ParkingApp.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingApp.Core.Interfaces;

public interface IParkingService
{
    Task<IReadOnlyList<ParkingSpot>> GetSpotsAsync(CancellationToken ct = default);
}
