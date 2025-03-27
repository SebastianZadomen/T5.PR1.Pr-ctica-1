using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using T5.PR1.Practica_1.Model;
using T5.PR1.Practica_1.Data;

namespace T5.PR1.Practica_1.Pages
{
    public class AddIndicadorEnergeModel : PageModel
    {
        private readonly EcoEnergyDbContext _context;

        [BindProperty]
        public EnergyIndicatorBD NuevoIndicador { get; set; } = new();

        public AddIndicadorEnergeModel(EcoEnergyDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            
            NuevoIndicador.Year = DateTime.Now.Year;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EnergyIndicators.Add(NuevoIndicador);
            _context.SaveChanges();

            return RedirectToPage("ShowIndicadorsEnergeModel");
        }
        private void RecalcularEstadisticas()
        {
            
            var prodNetaMayor3000 = _context.EnergyIndicators
                .Where(x => x.NetProduction > 3000)
                .OrderBy(x => x.NetProduction)
                .ToList();

            var gasolinaMayor100 = _context.EnergyIndicators
                .Where(x => x.GasolineConsumption > 100)
                .OrderByDescending(x => x.GasolineConsumption)
                .ToList();

            var mediaProduccionNeta = _context.EnergyIndicators
                .GroupBy(x => x.Year)
                .Select(g => new ShowIndicadorsEnergeModel.MediaProduccionPorAno
                {
                    Año = g.Key,
                    Promedio = g.Average(x => x.NetProduction)
                })
                .OrderBy(x => x.Año)
                .ToList();

            var demandaAltaBajaProduccion = _context.EnergyIndicators
                .Where(x => x.ElectricDemand > 4000 && x.AvailableProduction < 300)
                .ToList();

        }

    }
}