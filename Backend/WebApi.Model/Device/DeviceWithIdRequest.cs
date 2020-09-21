using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Device
{
     public class DeviceWithIdRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int LocationId { get; set; }
    }
}
