using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Connection : IBaseEntity
    {
        public int Id { get; set; }
        public virtual PersonalDevice PersonalDevice { get; set; }
        public int? PersonalDeviceId { get; set; }
        public virtual Device Device { get; set; }
        public int? DeviceId { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime FinishDateUTC { get; set; }

        public Connection()
        {
            StartDateUTC = DateTime.UtcNow;
        }
    }
}
