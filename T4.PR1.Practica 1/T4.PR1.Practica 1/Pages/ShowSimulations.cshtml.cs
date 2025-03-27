using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T5.PR1.Practica_1.Pages
{
    public class ShowSimulationsModel : PageModel
    {
        private readonly EcoEnergyDbContext _context;
        public List<SimulationBD> Simulations { get; set; } = new();

        public ShowSimulationsModel(EcoEnergyDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Simulations = _context.Simulations
                .OrderByDescending(s => s.Date)
                .ToList();
        }
    }
}