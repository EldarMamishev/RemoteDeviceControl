using Core.Entities.Base;
using Core.Entities.ApplicationIdentity;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class PersonalDevice : IBaseEntity
    {
        public int Id { get; set; }
        public Person User { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public UserDeviceType Type { get; set; }
    }
}
