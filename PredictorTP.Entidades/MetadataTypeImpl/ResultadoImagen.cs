using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades.EF;

public partial class ResultadoImagen
{
    public ResultadoImagen() { }
    public ResultadoImagen(string fileName, int userId) { 
        this.RutaImg = fileName;
        this.UserId = userId;
    }
}
