using Microsoft.AspNetCore.Mvc;

namespace PredictorTP.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            ViewData["login_o_register"] = true;
            return View();
        }


        [HttpPost]
        public IActionResult Ingresar(string usuario, string contrasenia)
        {
            return View();
        }

        public IActionResult Registrar()
        {
            ViewData["login_o_register"] = true;
            return View();
        }

        public IActionResult RecuperarContrasenia()
        {
            return Redirect("https://www.youtube.com/watch?v=-o849SEUQiQ");
        }
    }
}
