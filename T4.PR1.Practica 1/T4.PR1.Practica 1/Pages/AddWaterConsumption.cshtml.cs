using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Xml.Linq;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T5.PR1.Practica_1.Pages
{
    public class AddWaterConsumptionModel : PageModel
    {
        private readonly EcoEnergyDbContext _context;

        [BindProperty]
        public WaterConsumptionBD Consum { get; set; } = new();

        public AddWaterConsumptionModel(EcoEnergyDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WaterConsumptions.Add(Consum);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Consum d'aigua afegit correctament!";
            return RedirectToPage("ShowWaterConsumptions");
        }
    }
}