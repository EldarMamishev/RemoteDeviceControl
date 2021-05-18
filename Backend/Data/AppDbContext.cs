using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Core.Entities.ApplicationIdentity.Access;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Text;

namespace Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LogEntity> Logs{ get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<DeviceField> DeviceFields { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
            : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
