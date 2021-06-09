using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Connection
{
    public class ConnectionMaintenanceModel
    {
        public int DeviceId { get; set; }
        public int PersonId { get; set; }
        public int Time { get; set; }
        public TimeIdentifiersEnum TimeIdentifier { get; set; }
    }

    public enum TimeIdentifiersEnum
    {
        Seconds = 0,
        Minutes = 1,
        Hours = 2
    }
}
