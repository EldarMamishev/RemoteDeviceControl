using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Field;

namespace ViewModel.Device
{
    public class DeviceDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public IEnumerable<DeviceFieldModel> Fields { get; set; }
    }
}
