using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4.PR1.Practica_1;
namespace T4.PR1.Practica_1
{
    public class SolarSystem : EnergySystem
    {
        public double SunHours { get; set; }

        public override void Simulate(double parameter)
        {
            SunHours = parameter;
            EnergyGenerated = SunHours * 1.5;
        }
    }
}

