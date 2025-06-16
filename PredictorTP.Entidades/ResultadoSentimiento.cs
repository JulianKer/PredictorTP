using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades
{
    public class ResultadoSentimiento
    {
        public string _fraseConSentimiento { get; set; }
        public string _sentimiento { get; set; }

        public double _porcentajeDeConfianza;

        public ResultadoSentimiento(string fraseConSentimiento, string sentimiento, double porcentajeDeConfianza)
        {
            this._fraseConSentimiento = fraseConSentimiento;
            this._sentimiento = sentimiento;
            this._porcentajeDeConfianza = porcentajeDeConfianza;
        }
    }
}
