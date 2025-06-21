using Microsoft.AspNetCore.Mvc;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class UsuariosController : Controller
    {

        public IServicioUsuario _servicioUsuario;
        public UsuariosController(IServicioUsuario servicioUsuario) { 
            this._servicioUsuario = servicioUsuario;
        }

        public IActionResult Ver(string? busquedaUsuario)
        {
            ViewData["busquedaUsuario"] = busquedaUsuario;
            return View(this._servicioUsuario.GetUsuarios(busquedaUsuario));
        }

        public IActionResult Bloquear(int id) {
            if (id == 0 || !this._servicioUsuario.bloquear(id))
            {
                TempData["msjError"] = "No pudimos encontrar/bloquear el usuario.";
            }
            else
            {
                this._servicioUsuario.bloquear(id);
                TempData["msjExito"] = "Usuario bloqueado con éxito.";
            }
            return RedirectToAction("Ver");
        }

        public IActionResult Desbloquear(int id)
        {
            if (id == 0 || !this._servicioUsuario.desbloquear(id))
            {
                TempData["msjError"] = "No pudimos encontrar/desbloquear el usuario.";
            }
            else
            {
                this._servicioUsuario.desbloquear(id);
                TempData["msjExito"] = "Usuario desbloqueado con éxito.";
            }
            return RedirectToAction("Ver");
        }


        public IActionResult Convertir(int id)
        {
            if (id == 0 || !this._servicioUsuario.Convertir(id))
            {
                TempData["msjError"] = "No pudimos cambiar el rol del usuario.";
            }
            else
            {
                this._servicioUsuario.bloquear(id);
                TempData["msjExito"] = "Caambio de rol realizado con éxito.";
            }
            return RedirectToAction("Ver");
        }
    }
}
