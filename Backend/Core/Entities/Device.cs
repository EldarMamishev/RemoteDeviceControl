using Core.Entities.Base;
using Core.Entities.ApplicationIdentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Enums;

namespace Core.Entities
{
    public class Device : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public int? DeviceTypeId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocationId { get; set; }
        public virtual ICollection<LogEntity> Logs { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
        public virtual ICollection<DeviceField> DeviceFields { get; set; }
        public DeviceStatus Status { get; set; }
        public string ActiveState { get; set; }
    }
}
