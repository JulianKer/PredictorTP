using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.EF;
using PredictorTP.Models;
using PredictorTP.Servicios;
using PredictorTP.Session;

namespace PredictorTP.Controllers
{
    public class HomeController : Controller
    {
        private IServicioProcesarImagen _servicioProcesarImagen;     
         
        public HomeController(IServicioProcesarImagen servicioProcesarImagen)
        {
            this._servicioProcesarImagen = servicioProcesarImagen;
        }

        public IActionResult Index()
        {
            Usuario userSesion = HttpContext.Session.Get<Usuario>("USUARIO_LOGUEADO");
            EstadisticasAdminViewModel estadisticasAdminViewModel = new EstadisticasAdminViewModel();

            if (userSesion.Administrador) {
                estadisticasAdminViewModel.Labels = this._servicioProcesarImagen.ObtenerLabelEstadisticas();
                estadisticasAdminViewModel.Cantidades = this._servicioProcesarImagen.ObtenerCantidadEstadisticas();

                estadisticasAdminViewModel.LabelsUsuariosAdmin = this._servicioProcesarImagen.ObtenerLabelEstadisticasUsuariosAdmin();
                estadisticasAdminViewModel.CantidadesUsuariosAdmin = this._servicioProcesarImagen.ObtenerCantidadEstadisticasUsuariosAdmin();

                estadisticasAdminViewModel.LabelsPersonasEmociones = this._servicioProcesarImagen.ObtenerLabelEstadisticasPersonasEmociones();
                estadisticasAdminViewModel.CantidadesPersonasEmociones = this._servicioProcesarImagen.ObtenerCantidadEstadisticasPersonasEmociones();
            }
            return View(estadisticasAdminViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
