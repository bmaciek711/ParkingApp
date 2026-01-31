public class ParkingSpot
{
    public int Id { get; set; }
    public int Number { get; set; }
    public bool IsVipOnly { get; set; }
    public bool IsUnderMaintenance { get; set; }
    public string SpotType { get; set; } = "Samochód"; 
}