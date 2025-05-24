using Microsoft.EntityFrameworkCore;
using Converted_POS.Models;
using System;

namespace Converted_POS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Z Report related entities
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Till> Tills { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Transaction entity configuration
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductId);
                
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Till)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.TillId);
                
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);
                
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Location)
                .WithMany(l => l.Transactions)
                .HasForeignKey(t => t.LocationId);
                
            // Transaction Items relationship
            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.TransactionItems)
                .HasForeignKey(ti => ti.TransactionId);
                
            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Product)
                .WithMany()
                .HasForeignKey(ti => ti.ProductId);
        }
    }
} 