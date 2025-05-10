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

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("[DEBUG] ModelState inválido");
                return Page();
            }

            // Asegurar valores en los campos requeridos
            Simulation.SunHours = Simulation.SunHours ?? 0;
            Simulation.WindSpeed = Simulation.WindSpeed ?? 0;
            Simulation.WaterFlow = Simulation.WaterFlow ?? 0;

            Simulation.GeneratedEnergy = Simulation.Type switch
            {
                "Hydroelectric" => (double)Simulation.WaterFlow * 50, 
                "Solar" => (double)Simulation.SunHours * 10,          
                "Eolic" => (double)Simulation.WindSpeed * 5,         
                _ => 0
            };
            Simulation.Ratio = 0.2;
            Simulation.CostPerKWh = 0.1;
            Simulation.PricePerKWh = 0.15;
            Simulation.Date = DateTime.Now;

            try
            {
                _context.Simulations.Add(Simulation);
                int rowsAffected = _context.SaveChanges();
                Console.WriteLine($"[AddSimulation] Filas afectadas: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AddSimulation] Error al guardar: {ex.Message}");
                ModelState.AddModelError("", $"Error al guardar la simulación: {ex.Message}");
                return Page();
            }

            return RedirectToPage("/ShowSimulations");
        }
    }
}