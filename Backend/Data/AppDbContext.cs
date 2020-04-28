using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Core.Entities.ApplicationIdentity.Access;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PersonalDevice> PersonalDevices { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LogEntity> Logs{ get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
