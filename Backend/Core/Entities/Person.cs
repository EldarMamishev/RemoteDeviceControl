using System;
using System.Collections.Generic;
using Core.Entities.Base;
using Core.Entities.ApplicationIdentity;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Person : ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }

        public int? AccessGroupId { get; set; }
        public virtual AccessGroup AccessGroup { get; set; }

        public virtual ICollection<Connection> Connections { get; set; }
    }
}
