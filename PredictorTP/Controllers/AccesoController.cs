using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.EF;
using PredictorTP.Session;
using PredictorTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PredictorTP.Controllers
{
    [AllowAnonymous]
    public class AccesoController : Controller
    {
        private IServicioUsuario _servicioUsuario;

        public AccesoController(IServicioUsuario servicioUsuario)
        {
            this._servicioUsuario = servicioUsuario;
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
        public async Task<IActionResult> Ingresar(string email, string contrasenia) {
            var mensajeError = await _servicioUsuario.Login(email, contrasenia, HttpContext);

            if (mensajeError!= null) {

                ViewData["login_o_register"] = true;
                ViewBag.ErrorLogin = mensajeError;
                return View();
            }

            HttpContext.Session.Set<Usuario>("USUARIO_LOGUEADO", await this._servicioUsuario.buscarUsuarioPorEmail(email));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            ViewData["login_o_register"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario newUser, string confirmPassword)
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

            await this._servicioUsuario.Registrar(newUser);

            TempData["MensjaeExito"] = "Usuario creado con éxito, revise su correo para la verificación y luego inicie sesión.";
            return RedirectToAction("Ingresar");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarCuenta(string token)
        {
            var mensaje = await _servicioUsuario.ConfirmarCuenta(token);
            ViewBag.Mensaje = mensaje;
            ViewData["login_o_register"] = true;
            return View();
        }

        public IActionResult RecuperarContrasenia()
        {
            return Redirect("https://www.youtube.com/watch?v=-o849SEUQiQ");
        }

        public IActionResult Salir() {
            HttpContext.Session.Clear();
            TempData["MensjaeExito"] = "Sesión cerrada con éxito.";
            return RedirectToAction("Ingresar");
        }
    }
}
