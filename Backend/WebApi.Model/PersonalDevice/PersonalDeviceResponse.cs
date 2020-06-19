using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.PersonalDevice
{
    public class PersonalDeviceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonId { get; set; }
        public string DeviceType { get; set; }
    }
}
