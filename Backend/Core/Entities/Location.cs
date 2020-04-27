using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Location : IBaseEntity
    {
        public int Id { get; set; }
        public string MongoLocationId { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
