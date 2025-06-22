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

            if (Path.GetExtension(archivo.FileName).ToLower() != ".tsv")
                return BadRequest("Solo se permiten archivos con extensión .tsv.");

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
            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "idiomas.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                {
                    await writer.WriteLineAsync($"{fila.Text}\t{fila.Label}");
                }
            }

            string zipABorrar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_idiomas.zip");

            if (System.IO.File.Exists(zipABorrar))   System.IO.File.Delete(zipABorrar);

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count - 1,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count - 1
            });
        }






        [HttpPost]
        public async Task<IActionResult> ModeloSentimiento(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se cargó ningún archivo.");

            if (Path.GetExtension(archivo.FileName).ToLower() != ".tsv")
                return BadRequest("Solo se permiten archivos con extensión .tsv.");

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

                    ResultadoSentimiento resultadoSentimiento = this._servicioPredictorSentimiento.predecirSentimiento(texto);

                    if (resultadoSentimiento._sentimiento.ToLower().Equals(resultadoEsperado.ToLower()))
                        coincidencias.Add((texto, resultadoEsperado));
                    else
                        noCoincidencias.Add((texto, resultadoEsperado));
                }
            }

            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "sentimiento.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                {
                    await writer.WriteLineAsync($"{fila.Text}\t{fila.Label}");
                }
            }

            string zipABorrar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_sentimiento.zip");

            if (System.IO.File.Exists(zipABorrar)) System.IO.File.Delete(zipABorrar);

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count - 1,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count - 1
            });
        }




        [HttpPost]
        public async Task<IActionResult> ModeloPolaridad(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se cargó ningún archivo.");

            if (Path.GetExtension(archivo.FileName).ToLower() != ".tsv")
                return BadRequest("Solo se permiten archivos con extensión .tsv.");

            var coincidencias = new List<(string Text, bool Label)>();
            var noCoincidencias = new List<(string Text, bool Label)>();

            using (var stream = new StreamReader(archivo.OpenReadStream()))
            {
                await stream.ReadLineAsync(); 

                while (!stream.EndOfStream)
                {
                    var linea = await stream.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    var partes = linea.Split('\t');
                    if (partes.Length != 2)
                        continue;

                    string texto = partes[0].Trim();
                    bool resultadoEsperado = Convert.ToBoolean(partes[1].Trim());

                    var resultadoPolaridad = _servicioPredictorPolaridad.PredecirPolaridad(texto);
                    bool resultadoObtenido = resultadoPolaridad._resutlado.Trim().ToLower() == "positiva";

                    if (resultadoObtenido == resultadoEsperado)
                        coincidencias.Add((texto, resultadoEsperado));
                    else
                        noCoincidencias.Add((texto, resultadoEsperado));
                }
            }

            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "polaridad.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                    await writer.WriteLineAsync($"{fila.Text}\t{fila.Label}");
            }

            string zipABorrar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_polaridad.zip");
            if (System.IO.File.Exists(zipABorrar)) System.IO.File.Delete(zipABorrar);

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count
            });
        }



    }
}
