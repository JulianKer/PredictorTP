using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Entidades
{
    public partial class ResultadoPrediccion
    {
        public int ResultadoPrediccionId { get; set; }
        public string Texto { get; set; }
        public string TipoModelo { get; set; }
        public string Prediccion { get; set; }
        public decimal? Confianza { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
