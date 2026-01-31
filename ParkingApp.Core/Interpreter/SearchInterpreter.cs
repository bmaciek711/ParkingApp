namespace ParkingApp.Core.Interpreter;

//Interpreter Pattern

public class SearchContext
{
    public bool? OnlyVip { get; set; }
    public bool? OnlyAvailable { get; set; }

    public bool? OnlyMaintenance { get; set; }
}

public interface IExpression { void Interpret(SearchContext context); }

public class QueryExpression : IExpression
{
    private string _query;
    public QueryExpression(string query) => _query = query?.ToLower() ?? "";

    public void Interpret(SearchContext context)
    {
        if (_query.Contains("typ:vip") || _query.Contains("vip")) context.OnlyVip = true;
        if (_query.Contains("typ:standard") || _query.Contains("standard")) context.OnlyVip = false;

        if (_query.Contains("status:wolne") || _query.Contains("wolne") || _query.Contains("wolny"))
            context.OnlyAvailable = true;

        if (_query.Contains("status:serwis") || _query.Contains("serwis") || _query.Contains("naprawa"))
            context.OnlyMaintenance = true;
    }
}