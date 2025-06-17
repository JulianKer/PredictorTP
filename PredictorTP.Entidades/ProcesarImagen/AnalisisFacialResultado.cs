using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades.ProcesarImagen
{
    public class AnalisisFacialResultado
    {
        public List<string> Emociones { get; set; } = new();
        public List<RectanguloRostro> Rostros { get; set; } = new();
        public string ImagenGuardada { get; set; } = string.Empty;
    }
}
