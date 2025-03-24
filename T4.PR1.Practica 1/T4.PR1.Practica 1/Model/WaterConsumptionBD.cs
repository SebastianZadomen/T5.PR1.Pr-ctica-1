using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Practica_1.Model
{
    public class WaterConsumptionBD
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [Required]
        [StringLength(100)]
        public string Municipality { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double Consumption { get; set; }
    }
}
