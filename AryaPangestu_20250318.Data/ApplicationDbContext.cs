using AryaPangestu_20250318.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AryaPangestu_20250318.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStatusHistory> ShipmentStatusHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure shipment entity
            modelBuilder.Entity<Shipment>()
                .HasIndex(s => s.TrackingNumber)
                .IsUnique();

            // Configure status history entity
            modelBuilder.Entity<ShipmentStatusHistory>()
                .HasOne(sh => sh.Shipment)
                .WithMany(s => s.StatusHistory)
                .HasForeignKey(sh => sh.ShipmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
