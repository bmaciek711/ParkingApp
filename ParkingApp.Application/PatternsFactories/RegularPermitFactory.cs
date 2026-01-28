namespace ParkingApp.Application.PatternsFactories;

public sealed class RegularPermitFactory : IPermitFactory
{
    public AccessCode CreateAccessCode() => new BarCodeAccess();
    public MapGuide CreateMapGuide() => new OutdoorMapGuide();
}
