using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Xml.Linq;
using T4.PR1.Practica_1;
namespace T4.PR1.Practica_1.Pages
{
    public class AddSimulationModel : PageModel
    {
        [BindProperty]
        public string TipoSistema { get; set; } = "";

        [BindProperty]
        public double Parametro { get; set; }

        [BindProperty]
        public double CostoPorKWh { get; set; }

        [BindProperty]
        public double PrecioPorKWh { get; set; }

        public List<SelectListItem> TiposDeEnergia { get; } = new()
        {
            new SelectListItem { Value = "Hydroelectric", Text = "Hidroelectrico" },
            new SelectListItem { Value = "Solar", Text = "Solar" },
            new SelectListItem { Value = "Eolic", Text = "Eólico" }
        };

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            EnergySystem sistema = TipoSistema switch
            {
                "Hydroelectric" => new HydroelectricSystem(),
                "Solar" => new SolarSystem(),
                "Eolic" => new WindSystem(),
                _ => throw new InvalidOperationException("Tipo de sistema no válido")
            };

            sistema.Simulate(Parametro);

            double costoTotal = sistema.EnergyGenerated * CostoPorKWh;
            double precioTotal = sistema.EnergyGenerated * PrecioPorKWh;

            var simulacion = new SimulationResult
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                SystemType = TipoSistema,
                EnergyGenerated = sistema.EnergyGenerated,
                CostPerKWh = CostoPorKWh,
                PricePerKWh = PrecioPorKWh,
                TotalCost = costoTotal,
                TotalPrice = precioTotal
            };

            SimulationDataHandler.SaveSimulation(simulacion);

            return RedirectToPage("ShowSimulations");
        }
    }
}
