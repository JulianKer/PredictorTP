using PredictorTP.Entidades.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictorTP.Repositorios
{
    public interface IResultadoPrediccionRepositorio
    {
        Task GuardarResultado(ResultadoPrediccion resultado);
        List<ResultadoPrediccion> ObtenerResultadosPorUsuario(int usuarioId);
        List<ResultadoPrediccion> ObtenerResultadosPorUsuarioYTipo(int usuarioId, string tipoModelo);// por si despues queremos armar 
    }                                                                                                //un filtro por tipo de predict

    public class ResultadoPrediccionRepositorio : IResultadoPrediccionRepositorio
    {
        private readonly PredictorBddContext _context;

        public ResultadoPrediccionRepositorio(PredictorBddContext context)
        {
            _context = context;
        }

        public async Task GuardarResultado(ResultadoPrediccion resultado)
        {
            _context.ResultadoPrediccions.Add(resultado);
            await _context.SaveChangesAsync();
        }

        public List<ResultadoPrediccion> ObtenerResultadosPorUsuario(int usuarioId)
        {
            return _context.ResultadoPrediccions
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.Fecha)
                .ToList();
        }

        public List<ResultadoPrediccion> ObtenerResultadosPorUsuarioYTipo(int usuarioId, string tipoModelo)
        {
            return _context.ResultadoPrediccions
                .Where(r => r.UsuarioId == usuarioId && r.TipoModelo == tipoModelo)
                .OrderByDescending(r => r.Fecha)
                .ToList();
        }
    }
}
