using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Device
{
    public class DeviceRequest
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public int LocationId { get; set; }
    }
}
