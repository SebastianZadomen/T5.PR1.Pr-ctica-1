using Microsoft.AspNetCore.Mvc.RazorPages;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T5.PR1.Practica_1.Pages
{
    public class ShowWaterConsumptionsModel : PageModel
    {
        private readonly EcoEnergyDbContext _context;
        public List<WaterConsumptionBD> Consums { get; set; } = new();

        public ShowWaterConsumptionsModel(EcoEnergyDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Consums = _context.WaterConsumptions
                .OrderBy(w => w.Region)
                .ThenBy(w => w.Year)
                .ToList();
        }
    }
}