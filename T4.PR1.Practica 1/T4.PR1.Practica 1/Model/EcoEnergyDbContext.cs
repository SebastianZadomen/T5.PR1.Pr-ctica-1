using Microsoft.EntityFrameworkCore;
using T5.PR1.Practica_1.Model;

namespace T5.PR1.Practica_1.Data
{
    public class EcoEnergyDbContext : DbContext
    {
        public EcoEnergyDbContext(DbContextOptions<EcoEnergyDbContext> options) : base(options) { }

        public DbSet<SimulationBD> Simulations { get; set; }
        public DbSet<WaterConsumptionBD> WaterConsumptions { get; set; }
        public DbSet<EnergyIndicatorBD> EnergyIndicators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SimulationBD>()
                .Property(s => s.Type)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<WaterConsumptionBD>()
                .Property(w => w.Region)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<WaterConsumptionBD>()
                .Property(w => w.Municipality)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}