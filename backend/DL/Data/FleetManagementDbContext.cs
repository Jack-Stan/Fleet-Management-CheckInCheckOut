using BL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Data {
    public class FleetManagementDbContext : DbContext {
        public FleetManagementDbContext(DbContextOptions<FleetManagementDbContext> options) : base(options) {
        }

        public DbSet<Voertuig> Voertuigen { get; set; }
        public DbSet<Bestuurder> Bestuurders { get; set; }
        public DbSet<Reservering> Reserveringen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservering>()
                .HasOne<Bestuurder>()
                .WithMany(b => b.Reserveringen)
                .HasForeignKey(r => r.BestuurderId);

            modelBuilder.Entity<Reservering>()
                .HasOne<Voertuig>()
                .WithMany(v => v.Reserveringen)
                .HasForeignKey(r => r.VoertuigId);
        }
    }
}