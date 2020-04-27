using System;
using System.Collections.Generic;
using Core.Entities.Base;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Users
{
    public class User : IdentityUser<long>, IBaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }

    }
}
