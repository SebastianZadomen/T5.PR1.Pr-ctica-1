using System.ComponentModel.DataAnnotations;

namespace T5.PR1.Practica_1.Model
{
    public class SimulationBD
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [Range(0, 24)]
        public double? SunHours { get; set; } = 0;

        [Required]
        [Range(0, 200)]
        public double? WindSpeed { get; set; } = 0;

        [Required]
        [Range(0, 5000)]
        public double? WaterFlow { get; set; } = 0;

        [Required]
        [Range(0.1, 0.3)]
        public double Ratio { get; set; }

        [Required]
        public double GeneratedEnergy { get; set; }

        [Required]
        public double CostPerKWh { get; set; }

        [Required]
        public double PricePerKWh { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
