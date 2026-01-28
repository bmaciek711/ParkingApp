namespace ParkingApp.Application.PatternsInterpreter;

public sealed class NotMaintenanceExpression : IExpression
{
    public void Interpret(SearchContext context)
        => context.Spots = context.Spots.Where(s => !s.IsUnderMaintenance);
}
