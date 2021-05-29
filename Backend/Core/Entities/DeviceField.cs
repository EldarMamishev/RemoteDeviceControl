using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class DeviceField : IBaseEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? FieldId { get; set; }
        public virtual Field Field{ get; set; }
        public int? DeviceId { get; set; }
        public virtual Device Device{ get; set; }
    }
}
