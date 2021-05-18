using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Field : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public virtual ICollection<DeviceField> DeviceFields{ get; set; }
    }
}
