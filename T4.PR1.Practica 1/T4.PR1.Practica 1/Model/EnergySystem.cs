using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace T4.PR1.Practica_1
{
    public enum EnergyType
    {
        Hydroelectric,
        Eolic,
        Solar
    }
    public abstract class EnergySystem
    {
        public DateTime Date { get; set; }
        public double EnergyGenerated { get; set; }
        public EnergyType EType { get; protected set; }

        public EnergySystem()
        {
            Date = DateTime.Now;
        }

        public abstract void Simulate(double parameter);
    }
}
