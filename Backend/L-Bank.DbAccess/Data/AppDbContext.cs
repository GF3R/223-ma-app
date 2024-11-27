using L_Bank_W_Backend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace L_Bank_W_Backend.DbAccess.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Booking entity
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne(e => e.Source)
                    .WithMany()
                    .HasForeignKey(e => e.SourceId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.Destination)
                    .WithMany()
                    .HasForeignKey(e => e.DestinationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role)
                    .HasConversion<string>();
            });
        }
    }
}