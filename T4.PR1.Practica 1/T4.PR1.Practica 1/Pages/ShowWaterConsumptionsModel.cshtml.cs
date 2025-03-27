using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Xml.Linq;
using T5.PR1.Practica_1.Model;
using CsvHelper;
using CsvHelper.Configuration;
using T5.PR1.Practica_1.Data;

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