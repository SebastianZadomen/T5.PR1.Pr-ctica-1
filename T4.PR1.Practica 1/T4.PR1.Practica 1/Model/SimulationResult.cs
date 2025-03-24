using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4.PR1.Practica_1
{
    public class SimulationResult
    {
        public string Date { get; set; }
        public string SystemType { get; set; }
        public double EnergyGenerated { get; set; }
        public double CostPerKWh { get; set; }
        public double PricePerKWh { get; set; }
        public double TotalCost { get; set; }
        public double TotalPrice { get; set; }
    }
}