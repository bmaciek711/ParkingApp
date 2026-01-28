using MediatR;
using ParkingApp.Core.Interfaces;

using ParkingApp.Application.PatternsFactories;
using ParkingApp.Application.PatternsSingleton;
using ParkingApp.Core.Entities;


namespace ParkingApp.Application.PatternsMediator;

public sealed class CreateReservationHandler
    : IRequestHandler<CreateReservationCommand, ReservationResult>
{
    private readonly IRepository<ParkingSpot> _spots;
    private readonly IRepository<Reservation> _reservations;
    private readonly SystemConfiguration _config;

    public CreateReservationHandler(
        IRepository<ParkingSpot> spots,
        IRepository<Reservation> reservations,
        SystemConfiguration config)
    {
        _spots = spots;
        _reservations = reservations;
        _config = config;
    }


    public async Task<ReservationResult> Handle(
        CreateReservationCommand request,
        CancellationToken ct)
    {
        // 1️⃣ Walidacje
        if (request.DateFrom >= request.DateTo)
            return new(false, "Niepoprawny zakres dat.");

        if (request.DateFrom.TimeOfDay < _config.OpenHour ||
            request.DateTo.TimeOfDay > _config.CloseHour)
            return new(false, "Poza godzinami działania parkingu.");

        var spot = await _spots.GetByIdAsync(request.SpotId, ct);


        if (spot is null)
            return new(false, "Miejsce nie istnieje.");

        if (spot.IsUnderMaintenance)
            return new(false, "Miejsce jest w serwisie.");

        if (spot.IsVipOnly && !request.IsVipUser)
            return new(false, "Miejsce tylko dla VIP.");

        // 2️⃣ Sprawdzenie konfliktu

        var reservations = await _reservations.GetAllAsync(ct);

        var conflict = reservations.Any(r =>
    r.ParkingSpotId == request.SpotId &&
    r.IsActive &&
    request.DateFrom < r.EndTime &&
    request.DateTo > r.StartTime
);


        if (conflict)
            return new(false, "Miejsce jest zajęte.");

        // 3️⃣ Zapis rezerwacji
        await _reservations.AddAsync(new Reservation
        {
            VehicleId = request.VehicleId,
            ParkingSpotId = request.SpotId,
            StartTime = request.DateFrom,
            EndTime = request.DateTo,
            IsActive = true
        }, ct);


        // 4️⃣ ABSTRACT FACTORY – ticket
        IPermitFactory factory = request.IsVipUser
            ? new VipPermitFactory()
            : new RegularPermitFactory();

        var access = factory.CreateAccessCode();
        var map = factory.CreateMapGuide();

        return new ReservationResult(
            true,
            "Rezerwacja utworzona!",
            access.Type,
            access.Value,
            map.Format,
            map.Content
        );
    }
}
