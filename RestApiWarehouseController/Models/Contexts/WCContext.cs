using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestApiWarehouseController.Models;

namespace RestApiWarehouseController.Models.Contexts;

public partial class WCContext : DbContext
{
    public WCContext()
    {
    }

    public WCContext(DbContextOptions<WCContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07CCD1ACCD");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07BE4D2506");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Orders__UserId__2BFE89A6");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07C82717DD");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Order__30C33EC3");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__31B762FC");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07DB198DC7");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Products__Catego__2645B050");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Products__Suppli__2739D489");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Products__Wareho__25518C17");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC07BAA3B51E");

            entity.Property(e => e.ShipmentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Shipments)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Shipments__Suppl__367C1819");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Shipments)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Shipments__Wareh__37703C52");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC074E035C85");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC073FDAAE8C");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Warehous__3214EC07246D75E7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
