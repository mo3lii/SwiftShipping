using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Permissions;
using System.Reflection.Emit;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<DeliveryMan> DeliveryMans { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Government> Governments { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<WeightSetting> WeightSettings { get; set; } 
        public DbSet<DeliveryManRegions> DeliveryManRegions { get; set; }

        public DbSet<RolePermissions> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DeliveryManRegions>()
          .HasKey(d => new { d.DeliveryManId, d.RegionId });
            builder.Entity<WeightSetting>().HasData(new WeightSetting { Id=1,DefaultWeight=5,KGPrice=10});
            base.OnModelCreating(builder);

            builder.Entity<RolePermissions>()
        .HasKey(d => new { d.RoleName, d.DepartmentId });

            // Seed initial role permissions
            SeedRolePermissions(builder);

        }

        public void SeedRolePermissions(ModelBuilder builder)
        {
            var roles = RoleTypes.GetNames(typeof(RoleTypes)).ToList();
            
            var departments = Department.GetValues(typeof(Department)).Cast<Department>().ToList();

            foreach (var role in roles)
            {
                foreach (var department in departments)
                {
                    builder.Entity<RolePermissions>().HasData(new RolePermissions
                    {
                        RoleName = role,
                        DepartmentId = department,
                        View = false,
                        Edit = false,
                        Delete = false,
                        Add = false
                    });
                }
            }
        }
    }
}
