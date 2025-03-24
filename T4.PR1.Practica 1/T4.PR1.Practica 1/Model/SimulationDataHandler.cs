using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace T4.PR1.Practica_1
{
    public static class SimulationDataHandler
    {
        private static string filePath = "ModelData/simulaciones_energia.csv";
        public static void SaveSimulation(SimulationResult simulation)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            bool fileExists = File.Exists(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                if (!fileExists || new FileInfo(filePath).Length == 0)
                {
                    sw.WriteLine("Date|SystemType|EnergyGenerated|CostPerKWh|PricePerKWh|TotalCost|TotalPrice"); 
                }
                sw.WriteLine($"{simulation.Date}|{simulation.SystemType}|{simulation.EnergyGenerated}|{simulation.CostPerKWh}|{simulation.PricePerKWh}|{simulation.TotalCost}|{simulation.TotalPrice}");
            }
        }

        public static List<SimulationResult> LoadSimulations()
        {
            List<SimulationResult> simulations = new List<SimulationResult>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo no existe.");
                return simulations;
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                if (new FileInfo(filePath).Length == 0)
                {
                    Console.WriteLine("El archivo está vacío.");
                    return simulations;
                }

                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split('|'); 
                        if (parts.Length == 7)
                        {
                            try
                            {
                                var simulation = new SimulationResult
                                {
                                    Date = parts[0],
                                    SystemType = parts[1],
                                    EnergyGenerated = double.Parse(parts[2].Replace(",", "."), CultureInfo.InvariantCulture),
                                    CostPerKWh = double.Parse(parts[3].Replace(",", "."), CultureInfo.InvariantCulture),
                                    PricePerKWh = double.Parse(parts[4].Replace(",", "."), CultureInfo.InvariantCulture),
                                    TotalCost = double.Parse(parts[5].Replace(",", "."), CultureInfo.InvariantCulture),
                                    TotalPrice = double.Parse(parts[6].Replace(",", "."), CultureInfo.InvariantCulture)
                                };
                                simulations.Add(simulation);
                                Console.WriteLine($"Simulación cargada: {simulation.Date}, {simulation.SystemType}, {simulation.EnergyGenerated}");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Error de formato en la línea: {line}. Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Línea incorrecta: {line}. Número de columnas: {parts.Length}");
                        }
                    }
                }
            }
            return simulations;
        }
    }
}