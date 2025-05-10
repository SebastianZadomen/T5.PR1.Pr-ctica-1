using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using T4.PR1.Practica_1.Mapping;
using T5.PR1.Practica_1.Model;

namespace T5.PR1.Practica_1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EcoEnergyDbContext context)
        {
            try
            {
                Console.WriteLine("[DbInitializer] Iniciando inicialización de la base de datos...");

                context.Database.EnsureCreated();
                Console.WriteLine("[DbInitializer] Base de datos verificada/creada");

                LoadEnergyData(context);
                LoadWaterData(context);
                LoadSimulationData(context);

                Console.WriteLine("[DbInitializer] Inicialización completada con éxito");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DbInitializer] ERROR CRÍTICO: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[DbInitializer] Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        private static void LoadEnergyData(EcoEnergyDbContext context)
        {
            if (!context.EnergyIndicators.Any())
            {
                Console.WriteLine("[DbInitializer] Cargando datos de EnergyIndicators...");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ModelData", "indicadors_energetics_cat.csv");
                Console.WriteLine($"[DbInitializer] Ruta del archivo: {Path.GetFullPath(filePath)}");

                if (File.Exists(filePath))
                {
                    try
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ",",
                            HasHeaderRecord = true,
                            MissingFieldFound = null,
                            HeaderValidated = null,
                            BadDataFound = args => Console.WriteLine($"[DbInitializer] Dato inválido en fila {args.Context.Parser.Row}")
                        };

                        using var reader = new StreamReader(filePath);
                        using var csv = new CsvReader(reader, config);
                        csv.Context.RegisterClassMap<T5.PR1.Practica_1.Model.EnergyIndicatorBDMap>();
                        var records = csv.GetRecords<EnergyIndicatorBD>().ToList();
                        Console.WriteLine($"[DbInitializer] Registros leídos: {records.Count}");

                        context.EnergyIndicators.AddRange(records);
                        var saved = context.SaveChanges();
                        Console.WriteLine($"[DbInitializer] Registros guardados en BD: {saved}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[DbInitializer] Error al cargar EnergyIndicators: {ex.Message}");
                
                        if (ex is ReaderException readerEx && readerEx.Context != null)
                        {
                            Console.WriteLine($"[DbInitializer] Error en CSV: Fila {readerEx.Context.Parser.Row}");
                        }
                        else
                        {
                            Console.WriteLine("[DbInitializer] No se pudo determinar la fila exacta del error.");
                        }
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine($"[DbInitializer] Archivo CSV no encontrado en: {filePath}");
                }
            }
            else
            {
                Console.WriteLine("[DbInitializer] EnergyIndicators ya contiene datos, omitiendo carga");
            }
        }

        private static void LoadWaterData(EcoEnergyDbContext context)
        {
            if (!context.WaterConsumptions.Any())
            {
                Console.WriteLine("[DbInitializer] Cargando datos de WaterConsumptions...");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ModelData", "consum_aigua_cat_per_comarques.csv");
                Console.WriteLine($"[DbInitializer] Ruta del archivo: {filePath}");

                if (File.Exists(filePath))
                {
                    try
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ",",
                            HasHeaderRecord = true,
                            MissingFieldFound = null,
                            HeaderValidated = null,
                            BadDataFound = args => Console.WriteLine($"[DbInitializer] Dato inválido en fila {args.Context.Parser.Row}")
                        };

                        using var reader = new StreamReader(filePath);
                        using var csv = new CsvReader(reader, config);
                        csv.Context.RegisterClassMap<T4.PR1.Practica_1.Mapping.WaterConsumptionBDMap>();

                        var records = csv.GetRecords<WaterConsumptionBD>().ToList();
                        Console.WriteLine($"[DbInitializer] Registros leídos: {records.Count}");

                        context.WaterConsumptions.AddRange(records);
                        var saved = context.SaveChanges();
                        Console.WriteLine($"[DbInitializer] Registros guardados en BD: {saved}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[DbInitializer] Error al cargar WaterConsumptions: {ex.Message}");
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine("[DbInitializer] Archivo CSV de WaterConsumptions no encontrado!");
                }
            }
            else
            {
                Console.WriteLine("[DbInitializer] WaterConsumptions ya contiene datos, omitiendo carga");
            }
        }

        private static void LoadSimulationData(EcoEnergyDbContext context)
        {
            if (!context.Simulations.Any())
            {
                Console.WriteLine("[DbInitializer] Cargando datos de Simulations...");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ModelData", "simulaciones_energia.csv");
                Console.WriteLine($"[DbInitializer] Ruta del archivo: {filePath}");

                if (File.Exists(filePath))
                {
                    try
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ";",
                            HasHeaderRecord = true,
                            MissingFieldFound = null,
                            HeaderValidated = null,
                            BadDataFound = args => Console.WriteLine($"[DbInitializer] Dato inválido en fila {args.Context.Parser.Row}")
                        };

                        using var reader = new StreamReader(filePath);
                        using var csv = new CsvReader(reader, config);
                        csv.Context.RegisterClassMap<SimulationBDMap>();

                        var records = csv.GetRecords<SimulationBD>().ToList();
                        Console.WriteLine($"[DbInitializer] Registros leídos: {records.Count}");

                        var validRecords = records.Where(r =>
                            r.Ratio >= 0.1 && r.Ratio <= 0.3 &&
                            r.SunHours >= 0 && r.SunHours <= 24
                        ).ToList();

                        context.Simulations.AddRange(validRecords);
                        var saved = context.SaveChanges();
                        Console.WriteLine($"[DbInitializer] Registros guardados en BD: {saved}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[DbInitializer] Error al cargar Simulations: {ex.Message}");
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine("[DbInitializer] Archivo CSV de Simulations no encontrado!");
                }
            }
            else
            {
                Console.WriteLine("[DbInitializer] Simulations ya contiene datos, omitiendo carga");
            }
        }
    }
}