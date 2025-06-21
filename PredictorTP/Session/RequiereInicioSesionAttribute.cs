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
            // básicamente si el endpoint tiene un [allowanonimous], te deja seguir sin verificar si hay alguien logueado
            var tieneAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (tieneAllowAnonymous)
                return;

            // si NO tiene el allow anonimous, verifica primero si hay un user y: si NO hay un user, te manda al login,
            // si SÍ hay un user, te deja seguir al metodo q quiere ir
            var session = context.HttpContext.Session;
            var usuario = session.Get<Usuario>("USUARIO_LOGUEADO");

            if (usuario == null || (usuario != null && !usuario.Activo))
            {
                context.Result = new RedirectToActionResult("Ingresar", "Acceso", null);
            }   /*COMITEAR ESTO QUE YA ANDA, HICE LA FUNCION DE QUE SE BLOQUEA UN USER Y SE DESBLOQUEA Y ADEMAS VERIFICA AL LOGEARSE SI ESTÁS BLOQUEADO NO DEJARTE ENTRAR, 
                OJO, AGREGAR TAMBIEN QUE AL REGISTRAR UN USER M PRIMERO NO COINCIDA CON OTRO EMAIL Y ADEMAS EL ACTIVO POR DEFAULT EN TRUE YA QUE LO PONER DEFAULT EN FALSE*/
        }
    }
}
