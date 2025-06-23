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
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            Usuario usuario = this._servicioUsuario.buscarUsuarioPorId(userID);

            if (usuario == null)
            {
                return Redirect("/");
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Ver(Usuario editedUser, string confirmPassword)
        {
            HttpContext.Session.SetInt32("userID", 2); // BORRAR esta línea cuando tomi haga el login
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            Usuario userBdd = this._servicioUsuario.buscarUsuarioPorId(userID); 

            userBdd.Nombre = editedUser.Nombre;
            userBdd.Apellido = editedUser.Apellido;
            userBdd.Contrasenia = editedUser.Contrasenia;

            if(!ModelState.IsValid) {
                return View(userBdd);
            }

            if (!String.Equals(editedUser.Contrasenia, confirmPassword))
            {
                ViewBag.ErrorConfirmPassword = "Las contraseñas no coinciden!";
                return View(userBdd);
            }

            ViewBag.msj = this._servicioUsuario.ActualizarUsuario(userBdd); ;
            return View(userBdd);
        }


        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            if (id < 0)
            {
                return RedirectToAction("/");
            }

            this._servicioUsuario.eliminarUsuarioPorId(id);
            return RedirectToAction("/");
        }
    }
}
