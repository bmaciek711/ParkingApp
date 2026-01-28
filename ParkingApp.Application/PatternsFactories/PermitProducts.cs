namespace ParkingApp.Application.PatternsFactories;

public abstract class AccessCode
{
    public abstract string Type { get; }
    public abstract string Value { get; }
}

public abstract class MapGuide
{
    public abstract string Format { get; }
    public abstract string Content { get; }
}



public sealed class QrCodeAccess : AccessCode
{
    public override string Type => "QR";
    public override string Value { get; } = Guid.NewGuid().ToString("N");
}

public sealed class BarCodeAccess : AccessCode
{
    public override string Type => "BARCODE";
    public override string Value { get; } = $"BC-{Guid.NewGuid():N}";
}

public sealed class VipMapGuide : MapGuide
{
    public override string Format => "PDF";
    public override string Content => "VIP-Garage-Map.pdf";
}

public sealed class OutdoorMapGuide : MapGuide
{
    public override string Format => "JPG";
    public override string Content => "Outdoor-Parking-Map.jpg";
}
