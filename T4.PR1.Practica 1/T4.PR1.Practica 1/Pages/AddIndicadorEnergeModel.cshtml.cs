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

            return RedirectToPage("ShowIndicadorsEnerge");
        }
    }
}