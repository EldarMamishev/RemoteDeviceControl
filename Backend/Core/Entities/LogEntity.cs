using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class LogEntity : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Device Device { get; set; }
        public int? DeviceId { get; set; }
        public DateTime ActionTime { get; set; }
        public string Comments { get; set; }
    }
}
