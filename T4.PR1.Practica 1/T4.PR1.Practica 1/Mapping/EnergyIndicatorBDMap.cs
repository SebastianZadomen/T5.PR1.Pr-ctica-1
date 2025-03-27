using CsvHelper.Configuration;
using T5.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Mapping
{
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
}
