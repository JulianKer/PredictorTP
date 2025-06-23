using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades
{
    public class ResultadoPolaridad
    {
        public string _textoProcesado { get; set; }
        public string _resutlado { get; set; }
        public double _probabilidadNegativa { get; set; }
        public double _probabilidadPositiva { get; set; }

        public ResultadoPolaridad(string texto, string resultado, double probabilidadNegativa, double probabilidadPositiva)
        {
            this._textoProcesado = texto;
            this._resutlado = resultado;    
            this._probabilidadNegativa = probabilidadNegativa;
            this._probabilidadPositiva = probabilidadPositiva;
        }

    }
}
