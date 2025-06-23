using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorTP.Entidades;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{

    public interface IRepositorioPredictorIdioma
    {
        void GuardarResultadoIdioma(ResultadoIdioma nuevoResultadoLenguaje);
        List<ResultadoIdioma> ObtenerResultadosIdioma();
    }
    public class RepositorioPredictorIdioma : IRepositorioPredictorIdioma
    {

        private readonly PredictorBddContext _contexto;


        public RepositorioPredictorIdioma(PredictorBddContext contexto)
        {
            this._contexto = contexto;
        }

        public void GuardarResultadoIdioma(ResultadoIdioma ResultadoLenguaje)
        {
            var datoIdioma = new Entidades.EF.DatoIdioma
            {
                FraseEnIdioma = ResultadoLenguaje._fraseEnIdioma,
                Idioma = ResultadoLenguaje._idioma,
                PorcentajeDeConfianza = ResultadoLenguaje._porcentajeDeConfianza
            };
           _contexto.DatoIdiomas.Add(datoIdioma);
            _contexto.SaveChanges();
        }


        public List<ResultadoIdioma> ObtenerResultadosIdioma()
        {
            var resultados = _contexto.DatoIdiomas
                 .Select(d => new ResultadoIdioma(

                      d.FraseEnIdioma,
                      d.Idioma,
                      d.PorcentajeDeConfianza

                 ))
                 .ToList();
             return resultados;
        }

    }
}
