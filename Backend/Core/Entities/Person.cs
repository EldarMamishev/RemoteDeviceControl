using System;
using System.Collections.Generic;
using Core.Entities.Base;
using Core.Entities.Identity;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Person : ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<PersonalDevice> PersonalDevices { get; set; }
    }
}
