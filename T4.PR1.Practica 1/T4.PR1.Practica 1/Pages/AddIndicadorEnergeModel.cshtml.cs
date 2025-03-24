using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using T4.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Pages
{
    public class AddIndicadorEnergeModel : PageModel
    {
        [BindProperty]
        public IndicadorEnergetic NuevoIndicador { get; set; } = new();

        private const string CsvPath = "ModelData/indicadors_energetics_cat.csv";
        private const string JsonPath = "ModelData/indicadors_energetics_cat.json";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!System.IO.File.Exists(JsonPath))
            {
                var listaVacia = new List<IndicadorEnergetic>();
                var jsonInicial = JsonConvert.SerializeObject(listaVacia, Formatting.Indented);
                System.IO.File.WriteAllText(JsonPath, jsonInicial);
            }


            var jsonContent = System.IO.File.ReadAllText(JsonPath);
            var indicadoresExistentes = JsonConvert.DeserializeObject<List<IndicadorEnergetic>>(jsonContent);


            if (indicadoresExistentes == null)
            {
                ModelState.AddModelError(string.Empty, "El archivo de valores predeterminados no tiene un formato válido.");
                return Page();
            }

            var defaults = indicadoresExistentes.FirstOrDefault(); 
            NuevoIndicador = MergeWithDefaults(NuevoIndicador, defaults);

            indicadoresExistentes.Add(NuevoIndicador);

            var updatedJsonContent = JsonConvert.SerializeObject(indicadoresExistentes, Formatting.Indented);
            System.IO.File.WriteAllText(JsonPath, updatedJsonContent);

            var newLine = $"{NuevoIndicador.Data},{NuevoIndicador.PBEE_Hidroelectrica},{NuevoIndicador.CDEEBC_ProdBruta},{NuevoIndicador.CDEEBC_ProdNeta},{NuevoIndicador.CCAC_GasolinaAuto},{NuevoIndicador.CDEEBC_DemandaElectr},{NuevoIndicador.CDEEBC_ProdDisp}";

            System.IO.File.AppendAllText(CsvPath, "\n" + newLine);

            return RedirectToPage("ShowIndicadorsEnergeModel");
        }

        private IndicadorEnergetic MergeWithDefaults(IndicadorEnergetic partial, IndicadorEnergetic defaults)
        {
            if (defaults == null)
            {
                defaults = new IndicadorEnergetic
                {
                    Data = DateTime.Now.ToString("yyyy-MM-dd"),
                    CDEEBC_ProdNeta = 0.0,
                    CCAC_GasolinaAuto = 0.0,
                    CDEEBC_DemandaElectr = 0.0,
                    CDEEBC_ProdDisp = 0.0
                };
            }

            var merged = JsonConvert.DeserializeObject<IndicadorEnergetic>(JsonConvert.SerializeObject(defaults));

            merged.Data = partial.Data ?? defaults.Data;
            merged.CDEEBC_ProdNeta = partial.CDEEBC_ProdNeta ?? defaults.CDEEBC_ProdNeta;
            merged.CCAC_GasolinaAuto = partial.CCAC_GasolinaAuto ?? defaults.CCAC_GasolinaAuto;
            merged.CDEEBC_DemandaElectr = partial.CDEEBC_DemandaElectr ?? defaults.CDEEBC_DemandaElectr;
            merged.CDEEBC_ProdDisp = partial.CDEEBC_ProdDisp ?? defaults.CDEEBC_ProdDisp;

            return merged;
        }
    }
}