﻿using Core.Entities.ApplicationIdentity;
using Core.Entities.ApplicationIdentity.Access;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EfSettings
    {
        public static void SetSettings(ModelBuilder builder)
        {
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            builder.Entity<ApplicationRole>().ToTable("Roles")
                .Ignore(r => r.ConcurrencyStamp);
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            builder.Entity<ApplicationUser>().ToTable("Users");
            
            builder.Ignore<IdentityUserToken<int>>();
        }
    }
}