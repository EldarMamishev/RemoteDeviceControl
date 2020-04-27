using Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Users
{
    public class Device : IdentityUser<long>, IBaseEntity
    {
        public string NoSQLDeviceId { get; set; }

    }
}
