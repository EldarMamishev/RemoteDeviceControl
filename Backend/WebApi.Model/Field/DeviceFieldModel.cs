using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Field
{
    public class DeviceFieldModel
    {
        public int? Id { get; set; }
        public int FieldTypeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
