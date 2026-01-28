using ParkingApp.Core.Entities;


namespace ParkingApp.Core.Interfaces;

public interface IParkingSpotReadRepository
{
    Task<IReadOnlyList<ParkingSpot>> GetAvailableAsync(
        DateTime from, DateTime to, string? searchText, bool isVipUser, CancellationToken ct);


}
