using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Field;

namespace ViewModel.DeviceType
{
    public class DeviceTypeModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<FieldModel> Fields { get; set; }
    }
}
