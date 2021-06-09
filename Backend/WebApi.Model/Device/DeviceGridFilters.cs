using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Shared;

namespace ViewModel.Device
{
    public class DeviceGridFilters
    {
        public IEnumerable<GridFilterModel> DeviceTypes { get; set; }
        public IEnumerable<GridFilterModel> DeviceTypeFilters { get; set; }
    }
}
