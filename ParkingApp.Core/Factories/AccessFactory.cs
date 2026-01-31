using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.Core.Factories;

//Factory Method

public abstract class AccessMethod { public abstract string GetAccessData(); }
public class QrCodeAccess : AccessMethod { public override string GetAccessData() => $"QR-{Guid.NewGuid().ToString()[..8].ToUpper()}"; }
public class PinCodeAccess : AccessMethod { public override string GetAccessData() => new Random().Next(1000, 9999).ToString(); }

public abstract class AccessFactory { public abstract AccessMethod CreateMethod(); }
public class DigitalAccessFactory : AccessFactory { public override AccessMethod CreateMethod() => new QrCodeAccess(); }
public class ManualAccessFactory : AccessFactory { public override AccessMethod CreateMethod() => new PinCodeAccess(); }