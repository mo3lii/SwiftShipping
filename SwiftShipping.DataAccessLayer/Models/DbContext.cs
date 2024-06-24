using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<DeliveryMan> DeliveryMans { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Government> Governments { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<WeightSetting> WeightSettings { get; set; }
        public DbSet<DeliveryManRegions> DeliveryManRegions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
