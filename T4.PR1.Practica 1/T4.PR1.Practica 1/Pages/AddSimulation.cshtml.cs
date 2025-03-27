using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T5.PR1.Practica_1.Pages
{
    public class AddSimulationModel : PageModel
    {
        private readonly EcoEnergyDbContext _context;

        [BindProperty]
        public SimulationBD Simulation { get; set; } = new(); 

        public List<SelectListItem> TiposDeEnergia { get; } = new()
        {
            new SelectListItem { Value = "Hydroelectric", Text = "Hidroelectrico" },
            new SelectListItem { Value = "Solar", Text = "Solar" },
            new SelectListItem { Value = "Eolic", Text = "Eólico" }
        };

        public AddSimulationModel(EcoEnergyDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Calcula la energía generada según el tipo
            Simulation.GeneratedEnergy = Simulation.Type switch
            {
                "Hydroelectric" => Simulation.WaterFlow * 50,
                "Solar" => Simulation.SunHours * 10,
                "Eolic" => Simulation.WindSpeed * 5,
                _ => 0
            };

            Simulation.Date = DateTime.Now;

            _context.Simulations.Add(Simulation);
            _context.SaveChanges();

            return RedirectToPage("ShowSimulations");
        }
    }
}