using CsvHelper.Configuration;
using T5.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Mapping
{
    public class SimulationBDMap : ClassMap<SimulationBD>
    {
        public SimulationBDMap()
        {
            Map(m => m.Type).Name("TipoSistema");
            Map(m => m.SunHours).Name("HorasSol").Default(0);
            Map(m => m.WindSpeed).Name("VelocidadViento").Default(0);
            Map(m => m.WaterFlow).Name("CabalAigua").Default(0);
            Map(m => m.Ratio).Name("Rati").Default(0.2);
            Map(m => m.GeneratedEnergy).Name("EnergiaGenerada").Default(0);
            Map(m => m.CostPerKWh).Name("CostKWh").Default(0);
            Map(m => m.PricePerKWh).Name("PreuKWh").Default(0);
            Map(m => m.Date).Name("DataHora")
                .TypeConverterOption.Format("yyyy-MM-dd HH:mm:ss");
        }
    }
}
