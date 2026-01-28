namespace ParkingApp.Application.PatternsInterpreter;

public static class ExpressionInterpreter
{
    public static IReadOnlyList<IExpression> Parse(string query)
    {
        query = (query ?? "").ToLowerInvariant();
        var list = new List<IExpression>();

        if (query.Contains("vip"))
            list.Add(new VipExpression());

        // domyślnie ukryj serwis
        if (!query.Contains("serwis") && !query.Contains("maintenance"))
            list.Add(new NotMaintenanceExpression());

        return list;
    }
}
