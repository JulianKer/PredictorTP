using System;
using System.Collections.Generic;

namespace PredictorTP.Entidades.EF;

public partial class DatoPolaridad
{
    public int DatoPolaridadId { get; set; }

    public string? TextoProcesado { get; set; }

    public string? Resutlado { get; set; }

    public double? ProbabilidadNegativa { get; set; }

    public double? ProbabilidadPositiva { get; set; }
}
