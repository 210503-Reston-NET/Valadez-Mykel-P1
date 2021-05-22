using System;
using Microsoft.EntityFrameworkCore;
using Models;

#nullable disable

namespace DataLogic
{
    public partial class StoreDBContext : DbContext
    {
        public StoreDBContext()
        {
        }

        public StoreDBContext(DbContextOptions<StoreDBContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationProductInventory> LocationProductInventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public  DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LocationProductInventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId)
                    .HasName("PK__Location__F5FDE6B35F601849");

                // entity.HasOne(d => d.LocationId)
                //     .WithMany(p => p.LocationProductInventories)
                //     .HasForeignKey(d => d.LocationId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK__LocationP__Locat__7D439ABD");

                // entity.HasOne(d => d.ProductId)
                //     .WithMany(p => p.LocationProductInventories)
                //     .HasForeignKey(d => d.ProductId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK__LocationP__Produ__7C4F7684");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                //     entity.HasForeignKey(d => d.CustomerId)
                //     .HasConstraintName("FK__Orders__Customer__19DFD96B");

                // entity.HasOne(d => d.Location)
                //     .WithMany(p => p.Orders)
                //     .HasForeignKey(d => d.LocationId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK__Orders__Location__18EBB532");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId)
                    .HasName("PK__OrderDet__9DD74DBD6851E985");

                entity.Property(e => e.Delivered).HasDefaultValueSql("((0))");

                // entity.HasOne(d => d.Order)
                //     .WithMany(p => p.OrderDetails)
                //     .HasForeignKey(d => d.OrderId)
                //     .HasConstraintName("FK__OrderDeta__Order__22751F6C");

                // entity.HasOne(d => d.Product)
                //     .WithMany(p => p.OrderDetails)
                //     .HasForeignKey(d => d.ProductId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK__OrderDeta__Produ__2180FB33");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
