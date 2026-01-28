using MediatR;
using ParkingApp.Core.Entities;

namespace ParkingApp.Application.PatternsMediator;

public sealed record GetAvailableSpotsQuery(
    DateTime From,
    DateTime To,
    string SearchText,
    bool IsVipUser
) : IRequest<IReadOnlyList<ParkingSpot>>;
