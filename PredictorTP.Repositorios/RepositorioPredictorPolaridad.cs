using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using PredictorTP.Entidades;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{

    public interface IRepositorioPredictorPolaridad {

        void GuardarResultadoPolaridad(ResultadoPolaridad resultadoAGuardar);
        List<ResultadoPolaridad> GetResultadosPolaridad();
    
    
    }

    public class RepositorioPredictorPolaridad : IRepositorioPredictorPolaridad
    {

        private readonly PredictorBddContext _contexto;

        public RepositorioPredictorPolaridad(PredictorBddContext contexto)
        {

            this._contexto = contexto;
        }

        public void GuardarResultadoPolaridad(ResultadoPolaridad resultadoAGuardar)
        {
            var datoPolaridad = new Entidades.EF.DatoPolaridad
            {
                TextoProcesado = resultadoAGuardar._textoProcesado,
                Resutlado = resultadoAGuardar._resutlado,
                ProbabilidadNegativa= resultadoAGuardar._probabilidadNegativa,
                ProbabilidadPositiva = resultadoAGuardar._probabilidadPositiva,
            };

            _contexto.DatoPolaridads.Add(datoPolaridad);
            _contexto.SaveChanges();

        }

        public List<ResultadoPolaridad> GetResultadosPolaridad()
        {
            var resultados = _contexto.DatoPolaridads
                .Select(r => new ResultadoPolaridad(
                    r.TextoProcesado,
                    r.Resutlado,
                    r.ProbabilidadNegativa ?? 0,
                    r.ProbabilidadPositiva ?? 0))
                .ToList();
            return resultados;
        }









    }
}
