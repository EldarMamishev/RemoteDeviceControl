using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.PersonalDevice
{
    public class AddPersonalDeviceRequest
    {
        public int PersonId { get; set; }
        public string PersonalDeviceName { get; set; }
        public string DeviceType { get; set; }
    }
}
