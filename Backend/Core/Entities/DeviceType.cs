using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class DeviceType : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}
