using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades;
using PredictorTP.Entidades.EF;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class PredictorController : Controller
    {
        private IServicioPredictorPolaridad _servicioPredictorPolaridad { get; set; }
        private IServicioPredictorIdioma _servicioPredictorLenguaje { get; set; }
        private IServicioPredictorSentimiento _servicioPredictorSentimiento { get; set; }
        private IServicioProcesarImagen _servicioProcesarImagen { get; set; }

        private readonly IWebHostEnvironment _env;

        public PredictorController(IServicioPredictorPolaridad servicioPredictorPolaridad, 
                                   IServicioPredictorIdioma servicioPredictorLenguaje,
                                   IServicioPredictorSentimiento servicioPredictorSentimiento,
                                   IServicioProcesarImagen servicioProcesarImagen,
                                   IWebHostEnvironment env) { 
            this._servicioPredictorPolaridad = servicioPredictorPolaridad;
            this._servicioPredictorLenguaje = servicioPredictorLenguaje;
            this._servicioPredictorSentimiento = servicioPredictorSentimiento;
            this._servicioProcesarImagen = servicioProcesarImagen;
            this._env = env;
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


        // ProcesarImagen ----------------------------------------------------------
        public IActionResult ProcesarImagen()
        {
            //List<ResultadoImagen> historial = this._servicioProcesarImagen.GetImagenesDelUsuario(Convert.ToInt32(HttpContext.Session.GetInt32("userID")));
            // acá hay que añadir de pasarle este historial a la primer vista así me aparece el historial apenas entro a la vista y no cuando saco la foto
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Analizar([FromBody] ImagenConPersonasRequest request)
        {
            if (string.IsNullOrEmpty(request.ImagenBase64))
                return BadRequest("Imagen vacía");

            byte[] bytes = Convert.FromBase64String(request.ImagenBase64);

            List<string> Personas = request.Personas;

            string fileName = $"captura_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string folder = Path.Combine(_env.WebRootPath, "img/imgs_users");

            Directory.CreateDirectory(folder); // pensé que tenia que verificar si existia el directorio pero este metodo ya lo hace.

            string path = Path.Combine(folder, fileName);

            await System.IO.File.WriteAllBytesAsync(path, bytes);

            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            Console.WriteLine("USER ID ------------------------------->>>>>>>>>>>>>>>" + userID);

            this._servicioProcesarImagen.GuardarResultado(fileName, Personas, userID);

            return Json(this._servicioProcesarImagen.GetImagenesDelUsuario(userID));
        }

        //-----------------------------------------------------------------

    }
}
