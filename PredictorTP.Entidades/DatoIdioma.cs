using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace PredictorTP.Entidades
{
    public class DatoIdioma
    {
        [LoadColumn(0)]
        public string Text { get; set; }
        [LoadColumn(1)]
        public string Label { get; set; }
    }
}
