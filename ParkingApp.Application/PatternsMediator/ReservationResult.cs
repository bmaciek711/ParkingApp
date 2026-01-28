namespace ParkingApp.Application.PatternsMediator;

//Rezerwacja
public sealed record ReservationResult(
    bool Success,
    string Message,
    string? AccessCodeType = null,
    string? AccessCodeValue = null,
    string? MapFormat = null,
    string? MapContent = null
);
