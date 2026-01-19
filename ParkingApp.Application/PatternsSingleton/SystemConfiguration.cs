using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.Application.PatternsSingleton
{
    public sealed class SystemConfiguration
    {
        // Godziny otwarcia parkingu (np. 06:00 - 22:00)
        public TimeSpan OpenHour { get; set; } = new TimeSpan(6, 0, 0);
        public TimeSpan CloseHour { get; set; } = new TimeSpan(22, 0, 0);

        // Maksymalna ilość skarg przed nałożeniem blokady/banem
        public int MaxComplaintsToBan { get; set; } = 5;

    }
}
