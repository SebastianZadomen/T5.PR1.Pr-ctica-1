using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4.PR1.Practica_1;
namespace T4.PR1.Practica_1
{
    public class HydroelectricSystem : EnergySystem
    {
        public double WaterFlow { get; set; }

        public override void Simulate(double parameter)
        {
            WaterFlow = parameter;
            EnergyGenerated = WaterFlow * 9.8 * 0.8;
        }
    }
}