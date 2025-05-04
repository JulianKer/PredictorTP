using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades
{
    public class ResultadoLenguaje
    {
        public string _fraseEnIdioma { get; set; }
        public string _idioma { get; set; }

        public double _porcentajeDeConfianza;

        public ResultadoLenguaje(string fraseEnIdioma, string idioma, double porcentajeDeConfianza) { 
            this._fraseEnIdioma = fraseEnIdioma;
            this._idioma = idioma;
            this._porcentajeDeConfianza = porcentajeDeConfianza;
        }
    }
}
