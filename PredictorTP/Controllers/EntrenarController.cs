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

            /*var coincidencias = new List<(string Text, string Label)>();
            var noCoincidencias = new List<(string Text, string Label)>();
            var totales = new List<(string Text, string Label)>();*/
            
            var coincidencias = new List<ResultadoIdioma>();
            var noCoincidencias = new List<ResultadoIdioma>();
            var totales = new List<ResultadoIdioma>();

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
                    string resultadoEsperado = partes[1].Trim();

                    ResultadoIdioma resultadoIdioma = this._servicioPredictorLenguaje.predecirIdioma(texto);
                    ResultadoIdioma final = new ResultadoIdioma(texto, resultadoEsperado, 0);

                    if ( resultadoIdioma._idioma.ToLower().Equals(resultadoEsperado.ToLower()) )
                        coincidencias.Add(final);
                    else
                        noCoincidencias.Add(final);
                    
                    totales.Add(final);
                }
            }

            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "idiomas.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                {
                    await writer.WriteLineAsync($"{fila._fraseEnIdioma}\t{fila._idioma}");
                }
            }

            string zipABorrar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_idiomas.zip");

            if (System.IO.File.Exists(zipABorrar))   System.IO.File.Delete(zipABorrar);

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count,
                frasesCoincidencias = coincidencias,
                frasesNoCoincidencias = noCoincidencias,
                frasesTotales = totales
            });
        }






        [HttpPost]
        public async Task<IActionResult> ModeloSentimiento(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se cargó ningún archivo.");

            if (Path.GetExtension(archivo.FileName).ToLower() != ".tsv")
                return BadRequest("Solo se permiten archivos con extensión .tsv.");

            var coincidencias = new List<ResultadoSentimiento>();
            var noCoincidencias = new List<ResultadoSentimiento>();
            var totales = new List<ResultadoSentimiento>();

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
                    string resultadoEsperado = partes[1].Trim();

                    ResultadoSentimiento resultadoSentimiento = this._servicioPredictorSentimiento.predecirSentimiento(texto);
                    ResultadoSentimiento final = new ResultadoSentimiento(texto, resultadoEsperado, 0);

                    if (resultadoSentimiento._sentimiento.ToLower().Equals(resultadoEsperado.ToLower()))
                        coincidencias.Add(final);
                    else
                        noCoincidencias.Add(final);

                    totales.Add(final);
                }
            }

            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "sentimiento.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                {
                    await writer.WriteLineAsync($"{fila._fraseConSentimiento}\t{fila._sentimiento}");
                }
            }

            string zipABorrar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_sentimiento.zip");

            if (System.IO.File.Exists(zipABorrar)) System.IO.File.Delete(zipABorrar);

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count,
                frasesCoincidencias = coincidencias,
                frasesNoCoincidencias = noCoincidencias,
                frasesTotales = totales
            });
        }




        [HttpPost]
        public async Task<IActionResult> ModeloPolaridad(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se cargó ningún archivo.");

            if (Path.GetExtension(archivo.FileName).ToLower() != ".tsv")
                return BadRequest("Solo se permiten archivos con extensión .tsv.");

            var coincidencias = new List<ResultadoPolaridad>();
            var noCoincidencias = new List<ResultadoPolaridad>();
            var totales = new List<ResultadoPolaridad>();

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

                    ResultadoPolaridad resultadoPolaridad = _servicioPredictorPolaridad.PredecirPolaridad(texto);
                    bool resultadoObtenido = resultadoPolaridad._resutlado.Trim().ToLower() == "positiva";

                    ResultadoPolaridad final = new ResultadoPolaridad(texto, Convert.ToString(resultadoEsperado), 0.0, 0.0);

                    if (resultadoObtenido == resultadoEsperado)
                        coincidencias.Add(final);
                    else
                        noCoincidencias.Add(final);

                    totales.Add(final);
                }
            }

            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "polaridad.tsv");
            using (var writer = new StreamWriter(rutaArchivo, append: true))
            {
                foreach (var fila in coincidencias)
                    await writer.WriteLineAsync($"{fila._textoProcesado}\t{fila._resutlado}");
            }

            string zipABorrar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_polaridad.zip");
            if (System.IO.File.Exists(zipABorrar)) System.IO.File.Delete(zipABorrar);

            return Json(new
            {
                NoCoincidencias = noCoincidencias.Count,
                Coincidencias = coincidencias.Count,
                Total = coincidencias.Count + noCoincidencias.Count,
                frasesCoincidencias = coincidencias,
                frasesNoCoincidencias = noCoincidencias,
                frasesTotales = totales
            });
        }



    }
}
