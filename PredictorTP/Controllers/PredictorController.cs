using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades;
using PredictorTP.Entidades.ProcesarImagen;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class PredictorController : Controller
    {
        private IServicioPredictorPolaridad _servicioPredictorPolaridad { get; set; }
        private IServicioPredictorIdioma _servicioPredictorLenguaje { get; set; }
        private IServicioPredictorSentimiento _servicioPredictorSentimiento { get; set; }
        
        private readonly IWebHostEnvironment _env;
        private readonly IFaceEmotionService _faceService;

        public PredictorController(IServicioPredictorPolaridad servicioPredictorPolaridad, 
                                   IServicioPredictorIdioma servicioPredictorLenguaje,
                                   IServicioPredictorSentimiento servicioPredictorSentimiento,
                                   IWebHostEnvironment env, IFaceEmotionService faceService) { 

            this._servicioPredictorPolaridad = servicioPredictorPolaridad;
            this._servicioPredictorLenguaje = servicioPredictorLenguaje;
            this._servicioPredictorSentimiento = servicioPredictorSentimiento;

            this._env = env;
            this._faceService = faceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // polaridad ---------------------------------------------------
        public IActionResult Polaridad()
        {
            return View(this._servicioPredictorPolaridad.obtenerTodosLosResultados());
        }

        public IActionResult PredecirPolaridad(string texto)
        {
            ResultadoPolaridad nuevoResultado = this._servicioPredictorPolaridad.PredecirPolaridad(texto);
            this._servicioPredictorPolaridad.guardarResultado(nuevoResultado);

            return RedirectToAction("Polaridad");
        }
        // -------------------------------------------------------------------




        // idiomas ----------------------------------------------------------
        public IActionResult Idioma()
        {
            return View(this._servicioPredictorLenguaje.ObtenerResultadosIdioma());
        }

        [HttpPost]
        public IActionResult DetectarIdioma(string fraseEnIdioma)
        {
            ResultadoIdioma nuevoResultadoLenguaje = this._servicioPredictorLenguaje.predecirIdioma(fraseEnIdioma);
            this._servicioPredictorLenguaje.guardarResultdoIdioma(nuevoResultadoLenguaje);

            return RedirectToAction("Idioma");            
        }
        //-----------------------------------------------------------------




        // sentimiento ----------------------------------------------------------
        public IActionResult Sentimiento()
        {
            return View(this._servicioPredictorSentimiento.ObtenerResultadosSentimiento());
        }

        [HttpPost]
        public IActionResult DetectarSentimiento(string fraseConSentimiento)
        {
            ResultadoSentimiento nuevoResultadoSentimiento = this._servicioPredictorSentimiento.predecirSentimiento(fraseConSentimiento);
            this._servicioPredictorSentimiento.guardarResultdoSentimiento(nuevoResultadoSentimiento);

            return RedirectToAction("Sentimiento");
        }
        //-----------------------------------------------------------------


        // imagen  --------------------------------------------------------


        [HttpGet]
        public IActionResult ProcesarImagen()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Analizar([FromBody] ImagenBase64Request request)
        {
            if (string.IsNullOrEmpty(request.ImagenBase64))
                return BadRequest(new { error = "Imagen vacía" });

            try
            {
                byte[] bytes = Convert.FromBase64String(request.ImagenBase64);

                // Guardar archivo
                string fileName = $"captura_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string folder = Path.Combine(_env.WebRootPath, "img");
                Directory.CreateDirectory(folder);
                string path = Path.Combine(folder, fileName);

                await System.IO.File.WriteAllBytesAsync(path, bytes);

                // Enviar a Azure Face
                using var ms = new MemoryStream(bytes);
                var resultado = await _faceService.AnalizarDesdeStreamAsync(ms);
                resultado.ImagenGuardada = $"/img/{fileName}";

                return Json(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al analizar imagen: " + ex.Message);
                return StatusCode(500, new
                {
                    error = "Error al procesar la imagen",
                    mensaje = ex.Message
                });
            }
        }*/

        [HttpPost]
        public async Task<IActionResult> Analizar([FromBody] ImagenBase64Request request)
        {
            if (string.IsNullOrEmpty(request.ImagenBase64))
                return BadRequest("Imagen vacía");

            byte[] bytes = Convert.FromBase64String(request.ImagenBase64);

            // Guardar archivo
            string fileName = $"captura_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string folder = Path.Combine(_env.WebRootPath, "img");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, fileName);

            await System.IO.File.WriteAllBytesAsync(path, bytes);

            // Enviar a Azure Face
            using var ms = new MemoryStream(bytes);
            var resultado = await _faceService.AnalizarDesdeStreamAsync(ms);
            
            Console.WriteLine("errorRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR ::::::: " + resultado);
            resultado.ImagenGuardada = $"/img/{fileName}";

            return Json(resultado);
        }

        //-----------------------------------------------------------------







    }
}
