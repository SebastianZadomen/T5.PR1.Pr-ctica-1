using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Xml.Linq;
using T4.PR1.Practica_1.Model;
using CsvHelper;
using CsvHelper.Configuration;

namespace T4.PR1.Practica_1.Pages
{
    public class ShowWaterConsumptionsModel : PageModel
    {
        public List<WaterConsumption> Consums { get; set; } = new();

        public void OnGet()
        {
            var rutaCSV = "ModelData/consum_aigua_cat_per_comarques.csv";
            var rutaXML = "ModelData/consum_aigua_afegits.xml";

            using (var reader = new StreamReader(rutaCSV))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," }))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var consumo = new WaterConsumption
                    {
                        Any = csv.GetField<int>("Any"),
                        CodiComarca = csv.GetField<int>("Codi comarca"),
                        Comarca = csv.GetField<string>("Comarca"),
                        Poblacio = csv.GetField<int>("Població"),
                        DomesticXarxa = csv.GetField<int>("Domèstic xarxa"),
                        ActivitatsEconomiques = csv.GetField<int>("Activitats econòmiques i fonts pròpies"),
                        Total = csv.GetField<int>("Total"),
                        ConsumPerCapita = csv.GetField<double>("Consum domèstic per càpita")
                    };
                    Consums.Add(consumo);
                }
            }

            if (!System.IO.File.Exists(rutaXML) || new FileInfo(rutaXML).Length == 0)
            {

                if (System.IO.File.Exists(rutaXML))
                    System.IO.File.Delete(rutaXML);

                var docNuevo = new XDocument(new XElement("Consums"));
                docNuevo.Save(rutaXML);
            }

            XDocument doc;
            try
            {
                doc = XDocument.Load(rutaXML); 
                foreach (var x in doc.Root.Elements("Consum"))
                {
                    Consums.Add(ParseXML(x));
                }
            }
            catch (System.Xml.XmlException)
            {

                doc = new XDocument(new XElement("Consums"));
                doc.Save(rutaXML);
            }
        }


        private WaterConsumption ParseXML(XElement x) => new()
        {
            Any = (int)x.Element("Any"),
            CodiComarca = (int)x.Element("CodiComarca"),
            Comarca = (string)x.Element("Comarca"),
            Poblacio = (int)x.Element("Poblacio"),
            DomesticXarxa = (int)x.Element("DomesticXarxa"),
            ActivitatsEconomiques = (int)x.Element("ActivitatsEconomiques"),
            Total = (int)x.Element("Total"),
            ConsumPerCapita = (double)x.Element("ConsumPerCapita")
        };
    }
}