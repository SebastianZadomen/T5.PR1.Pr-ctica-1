using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Globalization;
using T4.PR1.Practica_1.Model;


namespace T4.PR1.Practica_1.Pages
{
    public class ShowIndicadorsEnergeModel : PageModel
    {
        public List<IndicadorEnergetic> IndicadoresEnergeticos { get; set; } = new();
        public List<IndicadorEnergetic> ProdNetaMayor3000 { get; set; } = new();
        public List<IndicadorEnergetic> GasolinaMayor100 { get; set; } = new();
        public List<MediaProduccionPorAno> MediaProduccionNeta { get; set; } = new();
        public List<IndicadorEnergetic> DemandaAltaBajaProduccion { get; set; } = new();
        public int TotalRegistros { get; set; }

        private const string CsvPath = "ModelData/indicadors_energetics_cat.csv";
        private const string JsonPath = "ModelData/indicadors_energetics_cat.json";

        public class MediaProduccionPorAno
        {
            public string Año { get; set; }
            public double Promedio { get; set; }
        }

        public void OnGet()
        {
            var registrosCSV = LeerCSV();
            var registrosJSON = LeerJSON();

            IndicadoresEnergeticos = registrosCSV.Concat(registrosJSON).ToList();
            TotalRegistros = IndicadoresEnergeticos.Count;

            CalcularEstadisticas();
        }

        private List<IndicadorEnergetic> LeerCSV()
        {
            var registros = new List<IndicadorEnergetic>();

            if (!System.IO.File.Exists(CsvPath)) 
                return registros;

            var lineas = System.IO.File.ReadAllLines(CsvPath);

            foreach (var linea in lineas.Skip(1))
            {
                var valores = linea.Split(',');

                if (valores.Length >= 39)
                {
                    registros.Add(new IndicadorEnergetic
                    {
                        Data = valores.Length > 0 ? valores[0] : DateTime.Now.ToString("yyyy-MM-dd"),
                        CDEEBC_ProdNeta = valores.Length > 9 ? ConvertirADouble(valores[9]) : 0.0,
                        CCAC_GasolinaAuto = valores.Length > 38 ? ConvertirADouble(valores[38]) : 0.0,
                        CDEEBC_ProdBruta = valores.Length > 7 ? ConvertirADouble(valores[7]) : 0.0,
                        CDEEBC_DemandaElectr = valores.Length > 14 ? ConvertirADouble(valores[14]) : 0.0,
                        CDEEBC_ProdDisp = valores.Length > 11 ? ConvertirADouble(valores[11]) : 0.0
                    });
                }
            }

            return registros;
        }

        private List<IndicadorEnergetic> LeerJSON()
        {
            if (!System.IO.File.Exists(JsonPath) || new FileInfo(JsonPath).Length == 0) 
            {
                var listaVacia = new List<IndicadorEnergetic>();
                var jsonInicial = JsonConvert.SerializeObject(listaVacia, Formatting.Indented);
                System.IO.File.WriteAllText(JsonPath, jsonInicial); 
                return listaVacia;
            }

            var jsonContent = System.IO.File.ReadAllText(JsonPath); 
            var registros = JsonConvert.DeserializeObject<List<IndicadorEnergetic>>(jsonContent);

            return registros ?? new List<IndicadorEnergetic>();
        }

        private void CalcularEstadisticas()
        {
            ProdNetaMayor3000 = IndicadoresEnergeticos
                .Where(x => x.CDEEBC_ProdNeta > 3000)
                .OrderBy(x => x.CDEEBC_ProdNeta)
                .ToList();

            GasolinaMayor100 = IndicadoresEnergeticos
                .Where(x => x.CCAC_GasolinaAuto > 100)
                .OrderByDescending(x => x.CCAC_GasolinaAuto)
                .ToList();

            MediaProduccionNeta = IndicadoresEnergeticos
                .GroupBy(x => ExtraerAnyo(x.Data))
                .Select(g => new MediaProduccionPorAno
                {
                    Año = g.Key,
                    Promedio = g.Average(x => x.CDEEBC_ProdNeta ?? 0)
                })
                .OrderBy(x => x.Año)
                .ToList();

            DemandaAltaBajaProduccion = IndicadoresEnergeticos
                .Where(x => x.CDEEBC_DemandaElectr > 4000 && x.CDEEBC_ProdDisp < 300)
                .ToList();
        }

        private string ExtraerAnyo(string data)
        {
            if (DateTime.TryParseExact(data, new[] { "MM/yyyy", "yyyy-MM-dd", "yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
            {
                return fecha.Year.ToString();
            }
            return "Desconocido";
        }

        private double ConvertirADouble(string valorStr)
        {
            valorStr = valorStr.Replace("%", "").Trim();
            return string.IsNullOrEmpty(valorStr) ? 0.0 : double.Parse(valorStr, CultureInfo.InvariantCulture);
        }
    }
}