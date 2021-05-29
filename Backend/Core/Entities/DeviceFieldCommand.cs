using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Base;

namespace Core.Entities
{
    public class DeviceFieldCommand : IBaseEntity
    {
        public int Id { get; set; }
        public int? CommandId { get; set; }
        public virtual Command Command { get; set; }
        public int? DeviceFieldId { get; set; }
        public virtual DeviceField DeviceField { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
