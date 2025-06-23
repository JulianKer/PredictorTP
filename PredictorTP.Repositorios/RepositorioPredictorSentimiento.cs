using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorTP.Entidades;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{
    public interface IRepositorioPredictorSentimiento
    {
        void GuardarSentimiento(ResultadoSentimiento ResultadoSentimiento);
        List<ResultadoSentimiento> ObtenerResultadosSentimiento();

    }
    public class RepositorioPredictorSentimiento : IRepositorioPredictorSentimiento
    {

        private readonly PredictorBddContext _contexto;

        public RepositorioPredictorSentimiento(PredictorBddContext contexto)
        {
            this._contexto = contexto;
        }


        public void GuardarSentimiento(ResultadoSentimiento ResultadoSentimiento)
        {
            var datoSentimiento = new Entidades.EF.DatoSentimiento
            {
                FraseConSentimiento = ResultadoSentimiento._fraseConSentimiento,
                Sentimiento = ResultadoSentimiento._sentimiento,
                PorcentajeDeConfianza = ResultadoSentimiento._porcentajeDeConfianza
            };
            _contexto.DatoSentimientos.Add(datoSentimiento);
            _contexto.SaveChanges();
        }

        public List<ResultadoSentimiento> ObtenerResultadosSentimiento()
        {
            var resultados = _contexto.DatoSentimientos
                .Select(d => new ResultadoSentimiento(
                    d.FraseConSentimiento,
                    d.Sentimiento,
                    d.PorcentajeDeConfianza
                ))
                .ToList();
            return resultados;
        }






    }
}
