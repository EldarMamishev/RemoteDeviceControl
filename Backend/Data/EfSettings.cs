using Core.Entities.ApplicationIdentity;
using Core.Entities.ApplicationIdentity.Access;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EfSettings
    {
        public static void SetSettings(ModelBuilder builder)
        {
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaim");
            builder.Entity<Role>().ToTable("Roles")
                .Ignore(r => r.ConcurrencyStamp);
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<long>>().ToTable("UserRole");
            builder.Entity<User>().ToTable("Users");

            builder.Ignore<IdentityUserToken<long>>();
        }
    }
}