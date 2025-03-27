using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T5.PR1.Practica_1;
namespace T5.PR1.Practica_1
{
    public class WindSystem : EnergySystem
    {
        public double WindSpeed { get; set; }

        public override void Simulate(double parameter)
        {
            WindSpeed = parameter;
            EnergyGenerated = Math.Pow(WindSpeed, 3) * 0.2;
        }
    }
}
