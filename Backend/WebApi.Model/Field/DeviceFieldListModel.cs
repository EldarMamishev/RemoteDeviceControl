using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Field
{
    public class DeviceFieldListModel
    {
        public int DeviceId { get; set; }
        public IEnumerable<DeviceFieldModel> Fields { get; set; }
    }
}
