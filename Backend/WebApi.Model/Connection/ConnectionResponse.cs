using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Connection
{
    public class ConnectionResponse
    {
        public int ConnectionId { get; set; }
        public int DeviceId { get; set; }
        public int PersonId { get; set; }
    }

    public class ConnectionViewModel
    {
        public int deviceId { get; set; }
        public int personId { get; set; }
    }
}
