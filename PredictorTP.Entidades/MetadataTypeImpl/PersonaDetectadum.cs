using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorTP.Entidades.EF;

public partial class PersonaDetectadum
{
    public PersonaDetectadum() { }
    public PersonaDetectadum(int idResultadoImagenGuardado, string descripcionPersona)
    {
        this.ResultadoImagenId = idResultadoImagenGuardado;
        this.DescripcionPersona = descripcionPersona;
    }
}
