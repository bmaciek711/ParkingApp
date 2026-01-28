namespace ParkingApp.Application.PatternsFactories;

public sealed class VipPermitFactory : IPermitFactory
{
    public AccessCode CreateAccessCode() => new QrCodeAccess();
    public MapGuide CreateMapGuide() => new VipMapGuide();
}
