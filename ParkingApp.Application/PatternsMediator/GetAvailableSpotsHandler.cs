using MediatR;
using ParkingApp.Core.Entities;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Application.PatternsMediator;

public sealed class GetAvailableSpotsHandler
    : IRequestHandler<GetAvailableSpotsQuery, IReadOnlyList<ParkingSpot>>
{
    private readonly IParkingSpotReadRepository _repo;

    public GetAvailableSpotsHandler(IParkingSpotReadRepository repo) => _repo = repo;

    public Task<IReadOnlyList<ParkingSpot>> Handle(GetAvailableSpotsQuery request, CancellationToken ct)
        => _repo.GetAvailableAsync(request.From, request.To, request.SearchText, request.IsVipUser, ct);
}
