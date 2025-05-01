
using Early_warning.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Early_warning.Contexts
{
    public class FloodContext : DbContext
    {
        public FloodContext(DbContextOptions<FloodContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<SeverityLevel> SeverityLevels { get; set; }
        public DbSet<FloodCause> FloodCauses { get; set; }
        public DbSet<FloodEvent> FloodEvents { get; set; }
        public DbSet<FloodEventDto> FloodEventDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
               .Property(l => l.Latitude)
               .HasColumnType("decimal(10,6)");

            modelBuilder.Entity<Location>()
                .Property(l => l.Longitude)
                .HasColumnType("decimal(10,6)");

            modelBuilder.Entity<Location>().HasIndex(l => l.City).IsUnique();
            modelBuilder.Entity<SeverityLevel>().HasIndex(s => s.Level).IsUnique();
            modelBuilder.Entity<FloodCause>().HasIndex(f => f.Cause).IsUnique();

            modelBuilder.Entity<FloodEvent>()
                .HasOne(f => f.Location)
                .WithMany()
                .HasForeignKey(f => f.LocationId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<FloodEvent>()
                .HasOne(f => f.Severity)
                .WithMany()
                .HasForeignKey(f => f.SeverityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FloodEvent>()
                .HasOne(f => f.Cause)
                .WithMany()
                .HasForeignKey(f => f.CauseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
