using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class PredictorController : Controller
    {
        private IServicioPredictorPolaridad _servicioPredictorSentimiento { get; set; }
        private IServicioPredictorLenguaje _servicioPredictorLenguaje { get; set; }
        public PredictorController(IServicioPredictorPolaridad servicioPredictorSentimiento, 
                                   IServicioPredictorLenguaje servicioPredictorLenguaje) { 
            this._servicioPredictorSentimiento = servicioPredictorSentimiento;
            this._servicioPredictorLenguaje = servicioPredictorLenguaje;
        }

        public IActionResult Index()
        {
            return View();
        }

        // sentimientos ---------------------------------------------------
        public IActionResult MostrarFormularioDePrediccionSentimiento()
        {
            return View(this._servicioPredictorSentimiento.obtenerTodosLosResultados());
        }

        public IActionResult PredecirSentimiento(string texto)
        {
            ResultadoPolaridad nuevoResultado = this._servicioPredictorSentimiento.PredecirSentimiento(texto);
            this._servicioPredictorSentimiento.guardarResultado(nuevoResultado);

            return RedirectToAction("MostrarFormularioDePrediccionSentimiento");
        }
        // -------------------------------------------------------------------




        // idiomas ----------------------------------------------------------
        public IActionResult MostrarFormularioDePrediccionLenguaje()
        {
            return View(this._servicioPredictorLenguaje.ObtenerResultadosLenguaje());
        }

        [HttpPost]
        public IActionResult DetectarIdioma(string fraseEnIdioma)
        {
            ResultadoLenguaje nuevoResultadoLenguaje = this._servicioPredictorLenguaje.predecirLenguaje(fraseEnIdioma);
            this._servicioPredictorLenguaje.guardarResultdoLenguaje(nuevoResultadoLenguaje);

            return RedirectToAction("MostrarFormularioDePrediccionLenguaje");            
        }
        //-----------------------------------------------------------------
    }
}
