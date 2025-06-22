using Microsoft.AspNetCore.Mvc;
using PredictorTP.Servicios;

namespace PredictorTP.Controllers
{
    public class UsuariosController : Controller
    {

        public IServicioUsuario _servicioUsuario;

        private readonly IWebHostEnvironment _env;

        public UsuariosController(IServicioUsuario servicioUsuario,
                                  IWebHostEnvironment env) { 
            this._servicioUsuario = servicioUsuario;
            this._env = env;
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
                TempData["msjExito"] = "Cambio de rol realizado con éxito.";
            }
            return RedirectToAction("Ver");
        }


        public IActionResult EliminarHistorialImagen()
        {
            this._servicioUsuario.EliminarHistorialImagen();
            string folder = Path.Combine(_env.WebRootPath, "img", "imgs_users");

            if (Directory.Exists(folder))
            {
                string[] archivos = Directory.GetFiles(folder);
                foreach (var archivo in archivos)
                {
                    System.IO.File.Delete(archivo);
                }
            }

            TempData["msjExito"] = "Historial de imágenes eliminado con éxito.";
            return RedirectToAction("Ver");
        }
    }
}
