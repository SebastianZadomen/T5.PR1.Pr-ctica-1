using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Practica_1.Model
{
    public class EnergyIndicatorBD
    {
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double NetProduction { get; set; }

        [Required]
        public double GasolineConsumption { get; set; }

        [Required]
        public double ElectricDemand { get; set; }

        [Required]
        public double AvailableProduction { get; set; }
    }
}
