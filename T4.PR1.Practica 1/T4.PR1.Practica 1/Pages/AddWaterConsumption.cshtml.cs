using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Xml.Linq;

namespace T4.PR1.Practica_1.Pages
{
    public class AddWaterConsumptionModel : PageModel
    {
        [BindProperty]
        public WaterConsumption Consum { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            string rutaXML = "ModelData/consum_aigua_afegits.xml";

            XDocument doc;
            if (System.IO.File.Exists(rutaXML))
                doc = XDocument.Load(rutaXML);
            else
                doc = new XDocument(new XElement("Consums"));

            var element = new XElement("Consum",
                new XElement("Any", Consum.Any),
                new XElement("CodiComarca", Consum.CodiComarca),
                new XElement("Comarca", Consum.Comarca),
                new XElement("Poblacio", Consum.Poblacio),
                new XElement("DomesticXarxa", Consum.DomesticXarxa),
                new XElement("ActivitatsEconomiques", Consum.ActivitatsEconomiques),
                new XElement("Total", Consum.Total),
                new XElement("ConsumPerCapita", Consum.ConsumPerCapita)
            );

            doc.Root.Add(element);
            doc.Save(rutaXML);

            return RedirectToPage("ShowWaterConsumptionsModel");
        }

        public class WaterConsumption
        {
            public int Any { get; set; }
            public int CodiComarca { get; set; }
            public string Comarca { get; set; }
            public int Poblacio { get; set; }
            public int DomesticXarxa { get; set; }
            public int ActivitatsEconomiques { get; set; }
            public int Total { get; set; }
            public double ConsumPerCapita { get; set; }
        }
    }
}