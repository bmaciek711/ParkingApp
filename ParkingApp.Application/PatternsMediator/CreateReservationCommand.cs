using MediatR;

namespace ParkingApp.Application.PatternsMediator;

//Rezerwacja
public sealed record CreateReservationCommand(
    string UserId,
    int VehicleId,
    int SpotId,
    DateTime DateFrom,
    DateTime DateTo,
    bool IsVipUser
) : IRequest<ReservationResult>;

