using System;
using System.Collections.Generic;

namespace PredictorTP.Entidades.EF;

public partial class ResultadoImagen
{
    public int ResultadoImagenId { get; set; }

    public int UserId { get; set; }

    public string RutaImg { get; set; } = null!;

    public virtual ICollection<PersonaDetectadum> PersonaDetectada { get; set; } = new List<PersonaDetectadum>();

    public virtual Usuario User { get; set; } = null!;
}
