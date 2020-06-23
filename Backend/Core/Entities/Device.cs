using Core.Entities.Base;
using Core.Entities.ApplicationIdentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Enums;

namespace Core.Entities
{
    public class Device : ApplicationUser
    {
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string MongoDeviceId { get; set; }
        public string MongoAccessDefinitionsId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocationId { get; set; }
        public virtual ICollection<LogEntity> Logs { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
    }
}
