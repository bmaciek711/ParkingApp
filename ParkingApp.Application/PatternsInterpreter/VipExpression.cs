namespace ParkingApp.Application.PatternsInterpreter;

public sealed class VipExpression : IExpression
{
    public void Interpret(SearchContext context)
        => context.Spots = context.Spots.Where(s => s.IsVipOnly);
}
