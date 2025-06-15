using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.EF;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class PerfilController : Controller
    {
        private IServicioUsuario _servicioUsuario;
        public PerfilController(IServicioUsuario servicioUsuario) {
            this._servicioUsuario = servicioUsuario;
        }


        [HttpGet]
        public IActionResult Ver()
        {

            //HACER: Acá obtener el ID del current User de la Sesion.-------------------------------
            Usuario usuario = this._servicioUsuario.buscarUsuarioPorId(1);

            if (usuario == null)
            {
                return Redirect("/");
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Ver(Usuario newUser, string confirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            if (!String.Equals(newUser.Contrasenia, confirmPassword))
            {
                ViewBag.ErrorConfirmPassword = "Las contraseñas no coinciden!";
                return View(newUser);
            }

            return RedirectToAction("Ver");
        }

    }
}
