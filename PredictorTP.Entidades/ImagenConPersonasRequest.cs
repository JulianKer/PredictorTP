using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades
{
    public class ImagenConPersonasRequest
    {
        public string ImagenBase64 { get; set; }
        public List<string> Personas { get; set; }
    }
}
