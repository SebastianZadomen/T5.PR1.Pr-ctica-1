using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace T4.PR1.Practica_1.Pages
{
    public class ShowSimulationsModel : PageModel
    {
        public List<SimulationResult> Simulations { get; set; } = new();

        public void OnGet()
        {
            Simulations = SimulationDataHandler.LoadSimulations();
        }
    }
}