using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
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
            builder.Entity<DeliveryManRegions>()
          .HasKey(d => new { d.DeliveryManId, d.RegionId });
            builder.Entity<WeightSetting>().HasData(new WeightSetting { DefaultWeight=5,KGPrice=10});
            base.OnModelCreating(builder);

        }
    }
}
