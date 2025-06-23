using System;
using System.Collections.Generic;

namespace PredictorTP.Entidades.EF;

public partial class DatoSentimiento
{
    public int ResultadoId { get; set; }

    public string FraseConSentimiento { get; set; } = null!;

    public string Sentimiento { get; set; } = null!;

    public double PorcentajeDeConfianza { get; set; }
}
