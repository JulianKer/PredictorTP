using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class PredictorController : Controller
    {
        private IServicioPredictor _servicioPredictor { get; set; }
        public PredictorController(IServicioPredictor servicioPredictor) { 
            this._servicioPredictor = servicioPredictor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MostrarFormularioDePrediccion()
        {
            return View(this._servicioPredictor.obtenerTodosLosResultados());
        }

        public IActionResult Predecir(string texto)
        {
            Resultado nuevoResultado = this._servicioPredictor.predecir(texto);
            this._servicioPredictor.guardarResultado(nuevoResultado);

            return RedirectToAction("MostrarFormularioDePrediccion");
        }
    }
}
