using System;
using System.Collections.Generic;

namespace PredictorTP.Entidades.EF;

public partial class FraseIdioma
{
    public int FraseIdiomaId { get; set; }

    public string FraseEnIdioma { get; set; } = null!;

    public string Idioma { get; set; } = null!;

    public double PorcentajeDeConfianza { get; set; }
}
