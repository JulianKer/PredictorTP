using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.EF;

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

            // ACÁ FALTA PEGARLE A LA BDD CREANDO EL USUARIO Y HACER LO DEL ENVIAR
            // MAIL PARA CONFIRMAR Y MARCAR EL CHECK DE VERIFICADO Y ASI TE DEJA INGREAR

            TempData["MensjaeExito"] = "Usuario creado con éxito, revise su correo para la verificación y luego inicie sesión.";
            return RedirectToAction("Ingresar");
        }
        public IActionResult RecuperarContrasenia()
        {
            return Redirect("https://www.youtube.com/watch?v=-o849SEUQiQ");
        }
    }
}
