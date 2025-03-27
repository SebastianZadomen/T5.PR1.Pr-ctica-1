using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations;

namespace T5.PR1.Practica_1.Model
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
    public class EnergyIndicatorBDMap : ClassMap<EnergyIndicatorBD>
    {
        public EnergyIndicatorBDMap()
        {
            Map(m => m.Year).Name("Any");
            Map(m => m.NetProduction).Name("ProduccioNeta");
            Map(m => m.GasolineConsumption).Name("ConsumGasolina");
            Map(m => m.ElectricDemand).Name("DemandaElectrica");
            Map(m => m.AvailableProduction).Name("ProduccioDisponible");
        }
    }

    public class WaterConsumptionBDMap : ClassMap<WaterConsumptionBD>
    {
        public WaterConsumptionBDMap()
        {
            Map(m => m.Region).Name("Comarca");
            Map(m => m.Municipality).Name("Municipi");
            Map(m => m.Year).Name("Any");
            Map(m => m.Consumption).Name("ConsumTotal");
        }
    }
}
