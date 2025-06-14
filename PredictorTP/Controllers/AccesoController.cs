using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.EF;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class AccesoController : Controller
    {
        private IServicioUsuario servicioUsuario;

        public AccesoController(IServicioUsuario servicioUsuario)
        {
            this.servicioUsuario = servicioUsuario;
        }

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

        [HttpGet]
        public IActionResult Registrar()
        {
            ViewData["login_o_register"] = true;
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario newUser, string confirmPassword)
        {
            if (!ModelState.IsValid) {

                ViewData["login_o_register"] = true;
                return View(newUser);
            }

            if (!String.Equals(newUser.Contrasenia, confirmPassword))
            {
                ViewData["login_o_register"] = true;
                ViewBag.ErrorConfirmPassword = "Las contraseñas no coinciden!";
                return View(newUser);
            }

            servicioUsuario.Registrar(newUser);

            TempData["MensjaeExito"] = "Usuario creado con éxito, revise su correo para la verificación y luego inicie sesión.";
            return RedirectToAction("Ingresar");
        }
        public IActionResult RecuperarContrasenia()
        {
            return Redirect("https://www.youtube.com/watch?v=-o849SEUQiQ");
        }
    }
}
