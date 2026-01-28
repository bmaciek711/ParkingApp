namespace ParkingApp.Application.PatternsFactories;

public interface IPermitFactory
{
    AccessCode CreateAccessCode();
    MapGuide CreateMapGuide();
}
