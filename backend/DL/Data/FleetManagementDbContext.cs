namespace DL.Data
{
    public class FleetManagementDbContext : DbContext
    {
        public FleetManagementDbContext(DbContextOptions<FleetManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Voertuig> Voertuigen { get; set; }
        public DbSet<Bestuurder> Bestuurders { get; set; }
        public DbSet<Reservering> Reserveringen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservering>()
                .HasOne<Bestuurder>()
                .WithMany(b => b.Reserveringen)
                .HasForeignKey(r => r.BestuurderId);

            modelBuilder.Entity<Reservering>()
                .HasOne<Voertuig>()
                .WithMany(v => v.Reserveringen)
                .HasForeignKey(r => r.VoertuigId);

            // Als je wil dat de lijst van afbeeldingen correct wordt opgeslagen
            modelBuilder.Entity<Reservering>()
                .Property(r => r.CheckOutPictures)
                .HasConversion(
                    v => string.Join(";", v), // Lijst omzetten naar string
                    v => v.Split(";", StringSplitOptions.None).ToList() // String omzetten naar lijst
                );

            modelBuilder.Entity<Reservering>()
                .Property(r => r.CheckInPictures)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(";", StringSplitOptions.None).ToList()
                );
        }
    }
}
