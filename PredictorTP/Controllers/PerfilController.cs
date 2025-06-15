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
        public IActionResult Ver(Usuario editedUser, string confirmPassword)
        {
            Usuario userBdd = this._servicioUsuario.buscarUsuarioPorId(1); //ACA TIENE QUE IR LA VARIABLE DEL ID USER DE LA SESION
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


        [HttpGet]
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
