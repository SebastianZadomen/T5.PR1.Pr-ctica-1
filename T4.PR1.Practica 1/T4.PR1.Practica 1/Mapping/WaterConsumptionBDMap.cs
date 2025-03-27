using CsvHelper.Configuration;
using T5.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Mapping
{
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
