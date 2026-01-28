using ParkingApp.Core.Entities;

namespace ParkingApp.Application.PatternsInterpreter;

public sealed class SearchContext
{
    public string Query { get; }
    public IQueryable<ParkingSpot> Spots { get; set; }
    public DateTime From { get; }
    public DateTime To { get; }

    public SearchContext(string query, IQueryable<ParkingSpot> spots, DateTime from, DateTime to)
    {
        Query = query ?? "";
        Spots = spots;
        From = from;
        To = to;
    }
}
