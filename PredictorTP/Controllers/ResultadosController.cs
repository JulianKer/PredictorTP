using Microsoft.AspNetCore.Mvc;
using PredictorTP.Servicios;
using Microsoft.AspNetCore.Http;

public class ResultadosController : Controller
{
    private readonly IServicioResultadoPrediccion _servicioResultados;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResultadosController(IServicioResultadoPrediccion servicioResultados, IHttpContextAccessor httpContextAccessor)
    {
        _servicioResultados = servicioResultados;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("userId");
        if (!userId.HasValue) return RedirectToAction("Ingresar", "Acceso");

        var resultadosIdioma = _servicioResultados.ObtenerResultadosPorUsuarioYTipo(userId.Value, "idioma");
        var resultadosSentimiento = _servicioResultados.ObtenerResultadosPorUsuarioYTipo(userId.Value, "sentimiento");
        var resultadosPolaridad = _servicioResultados.ObtenerResultadosPorUsuarioYTipo(userId.Value, "polaridad");

        var model = new
        {
            ResultadosIdioma = resultadosIdioma,
            ResultadosSentimiento = resultadosSentimiento,
            ResultadosPolaridad = resultadosPolaridad
        };

        return View(model);
    }
}