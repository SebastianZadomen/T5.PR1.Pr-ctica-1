using System.Globalization;

namespace T4.PR1.Practica_1.Model
{

    public class WaterConsumption
    {
        public int Any { get; set; }
        public int CodiComarca { get; set; }
        public string Comarca { get; set; }
        public int Poblacio { get; set; }
        public int DomesticXarxa { get; set; }
        public int ActivitatsEconomiques { get; set; }
        public int Total { get; set; }
        public double ConsumPerCapita { get; set; }
    }

    public class IndicadorEnergetic
    {
        public string Data { get; set; }
        public double? PBEE_Hidroelectrica { get; set; }
        public double CDEEBC_ProdBruta { get; set; }
        public double? CDEEBC_ProdNeta { get; set; }
        public double? CDEEBC_ProdDisp { get; set; }
        public double? CDEEBC_DemandaElectr { get; set; }
        public double? CCAC_GasolinaAuto { get; set; }


    }
}

