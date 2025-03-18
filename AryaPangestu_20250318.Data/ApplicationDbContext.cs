using System;
using System.Collections.Generic;
using AryaPangestu_20250318.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AryaPangestu_20250318.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<ShipmentStatusHistory> ShipmentStatusHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.ToTable("Shipment");

            entity.HasIndex(e => e.TrackingNumber, "UK_Shipment_TrackingNumber").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Created");
            entity.Property(e => e.Height).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Length).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RecipientAddress).HasMaxLength(255);
            entity.Property(e => e.RecipientName).HasMaxLength(100);
            entity.Property(e => e.RecipientPhone).HasMaxLength(20);
            entity.Property(e => e.RecipientPostCode).HasMaxLength(10);
            entity.Property(e => e.SenderAddress).HasMaxLength(255);
            entity.Property(e => e.SenderName).HasMaxLength(100);
            entity.Property(e => e.SenderPhone).HasMaxLength(20);
            entity.Property(e => e.SenderPostCode).HasMaxLength(10);
            entity.Property(e => e.TrackingNumber).HasMaxLength(20);
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Width).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ShipmentStatusHistory>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("ShipmentStatusHistory");

            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StatusDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Shipment).WithMany(p => p.ShipmentStatusHistories)
                .HasForeignKey(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShipmentStatusHistory_Shipment");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
