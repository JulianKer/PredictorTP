using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictorTP.Servicios
{
    public interface IServicioResultadoPrediccion
    {
        Task GuardarResultado(ResultadoPrediccion resultado);
        List<ResultadoPrediccion> ObtenerResultadosPorUsuario(int usuarioId);
        List<ResultadoPrediccion> ObtenerResultadosPorUsuarioYTipo(int usuarioId, string tipoModelo);
    }



    public class ServicioResultadoPrediccion : IServicioResultadoPrediccion
    {
        private readonly IResultadoPrediccionRepositorio _repo;

        public ServicioResultadoPrediccion(IResultadoPrediccionRepositorio repo)
        {
            _repo = repo;
        }

        public async Task GuardarResultado(ResultadoPrediccion resultado)
        {
            await _repo.GuardarResultado(resultado);
        }

        public List<ResultadoPrediccion> ObtenerResultadosPorUsuario(int usuarioId)
        {
            return _repo.ObtenerResultadosPorUsuario(usuarioId);
        }

        public List<ResultadoPrediccion> ObtenerResultadosPorUsuarioYTipo(int usuarioId, string tipoModelo)
        {
            return _repo.ObtenerResultadosPorUsuarioYTipo(usuarioId, tipoModelo);
        }
    }
}