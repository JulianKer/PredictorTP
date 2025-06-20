using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.EF;
using Microsoft.AspNetCore.Authorization;

namespace PredictorTP.Session
{
    public class RequiereInicioSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var tieneAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (tieneAllowAnonymous)
                return;

            var session = context.HttpContext.Session;
            var usuario = session.Get<Usuario>("USUARIO_LOGUEADO");

            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Ingresar", "Acceso", null);
            }
        }
    }
}
