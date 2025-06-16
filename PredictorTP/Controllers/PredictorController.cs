using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class PredictorController : Controller
    {
        private IServicioPredictorPolaridad _servicioPredictorPolaridad { get; set; }
        private IServicioPredictorIdioma _servicioPredictorLenguaje { get; set; }
        public PredictorController(IServicioPredictorPolaridad servicioPredictorSentimiento, 
                                   IServicioPredictorIdioma servicioPredictorLenguaje) { 
            this._servicioPredictorPolaridad = servicioPredictorSentimiento;
            this._servicioPredictorLenguaje = servicioPredictorLenguaje;
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
    }
}
