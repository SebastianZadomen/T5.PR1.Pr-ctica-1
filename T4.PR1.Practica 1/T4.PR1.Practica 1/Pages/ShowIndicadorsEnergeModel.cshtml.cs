using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Globalization;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;


namespace T5.PR1.Practica_1.Pages
{
    public class ShowIndicadorsEnergeModel : PageModel
    {
        private readonly EcoEnergyDbContext _context;

        public List<EnergyIndicatorBD> IndicadoresEnergeticos { get; set; } = new();
        public List<EnergyIndicatorBD> ProdNetaMayor3000 { get; set; } = new();
        public List<EnergyIndicatorBD> GasolinaMayor100 { get; set; } = new();
        public List<MediaProduccionPorAno> MediaProduccionNeta { get; set; } = new();
        public List<EnergyIndicatorBD> DemandaAltaBajaProduccion { get; set; } = new();
        public int TotalRegistros { get; set; }

        public ShowIndicadorsEnergeModel(EcoEnergyDbContext context)
        {
            _context = context;
        }

        public class MediaProduccionPorAno
        {
            public int Año { get; set; }
            public double Promedio { get; set; }
        }

        public void OnGet()
        {
            IndicadoresEnergeticos = _context.EnergyIndicators.ToList();
            TotalRegistros = IndicadoresEnergeticos.Count;

            CalcularEstadisticas();
        }

        private void CalcularEstadisticas()
        {
            ProdNetaMayor3000 = _context.EnergyIndicators
                .Where(x => x.NetProduction > 3000)
                .OrderBy(x => x.NetProduction)
                .ToList();

            GasolinaMayor100 = _context.EnergyIndicators
                .Where(x => x.GasolineConsumption > 100)
                .OrderByDescending(x => x.GasolineConsumption)
                .ToList();

            MediaProduccionNeta = _context.EnergyIndicators
                .GroupBy(x => x.Year)
                .Select(g => new MediaProduccionPorAno
                {
                    Año = g.Key,
                    Promedio = g.Average(x => x.NetProduction)
                })
                .OrderBy(x => x.Año)
                .ToList();

            DemandaAltaBajaProduccion = _context.EnergyIndicators
                .Where(x => x.ElectricDemand > 4000 && x.AvailableProduction < 300)
                .ToList();
        }
    }
}