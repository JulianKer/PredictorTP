using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades;
using PredictorTP.Servicios;
using static System.Net.Mime.MediaTypeNames;

namespace PredictorTP.Controllers
{
    public class EntrenarController : Controller
    {
        private IServicioPredictorPolaridad _servicioPredictorPolaridad;
        private IServicioPredictorIdioma _servicioPredictorLenguaje;
        private IServicioPredictorSentimiento _servicioPredictorSentimiento;

        public EntrenarController(IServicioPredictorPolaridad servicioPredictorPolaridad,
                                   IServicioPredictorIdioma servicioPredictorLenguaje,
                                   IServicioPredictorSentimiento servicioPredictorSentimiento)
        {
            this._servicioPredictorPolaridad = servicioPredictorPolaridad;
            this._servicioPredictorLenguaje = servicioPredictorLenguaje;
            this._servicioPredictorSentimiento = servicioPredictorSentimiento;
        }

        public IActionResult Modelos() { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModeloIdioma(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se cargó ningún archivo.");

            var coincidencias = new List<(string Text, string Label)>();
            var noCoincidencias = new List<(string Text, string Label)>();

            using (var stream = new StreamReader(archivo.OpenReadStream()))
            {
                int i = 0;
                while (!stream.EndOfStream)
                {
                    i++;
                    if (i == 1) continue;

                    var linea = await stream.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(linea))
                        continue; // ignora líneas vacías

                    var partes = linea.Split('\t'); // TSV usa tabulación
                    if (partes.Length != 2)
                        continue; // ignorar líneas mal formateadas

                    string texto = partes[0].Trim();
                    string resultadoEsperado = partes[1].Trim();

                    ResultadoIdioma resultadoIdioma = this._servicioPredictorLenguaje.predecirIdioma(texto);

                    if ( resultadoIdioma._idioma.ToLower().Equals(resultadoEsperado.ToLower()) )
                        coincidencias.Add((texto, resultadoEsperado));
                    else
                        noCoincidencias.Add((texto, resultadoEsperado));
                }
            }

            // Guardar coincidencias en un archivo .tsv EXISTENTE
            //string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entrenamiento", "idiomas.tsv");
            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "idiomas.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                {
                    await writer.WriteLineAsync($"{fila.Text}\t{fila.Label}");
                }
            }

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count - 1,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count - 1
            });
        }
    }
}
