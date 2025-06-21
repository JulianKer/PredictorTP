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
    }
}
