#nullable disable

using Invoicify.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoicify.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
            });

            // Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Email)
                      .HasMaxLength(150);

                entity.Property(c => c.Contact)
                      .HasMaxLength(20);
            });

            // Invoice
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasOne(i => i.Customer)
                      .WithMany()
                      .HasForeignKey(i => i.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(i => i.TotalAmount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
            });

            // InvoiceLineItem
            modelBuilder.Entity<InvoiceLineItem>(entity =>
            {
                entity.HasOne(li => li.Invoice)
                      .WithMany(i => i.LineItems)
                      .HasForeignKey(li => li.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(li => li.Product)
                      .WithMany()
                      .HasForeignKey(li => li.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(li => li.UnitPrice)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(li => li.Quantity)
                      .IsRequired();
            });
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}
