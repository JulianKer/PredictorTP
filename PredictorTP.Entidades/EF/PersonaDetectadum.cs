using System;
using System.Collections.Generic;

namespace PredictorTP.Entidades.EF;

public partial class PersonaDetectadum
{
    public int PersonaDetectadaId { get; set; }

    public int ResultadoImagenId { get; set; }

    public string DescripcionPersona { get; set; } = null!;

    public virtual ResultadoImagen ResultadoImagen { get; set; } = null!;
}
