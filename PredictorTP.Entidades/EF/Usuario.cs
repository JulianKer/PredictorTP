using System;
using System.Collections.Generic;

namespace PredictorTP.Entidades.EF;

public partial class Usuario
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Tokenconfirmacion { get; set; }

    public string Contrasenia { get; set; } = null!;

    public bool Activo { get; set; }

    public bool Verificado { get; set; }

    public bool Administrador { get; set; }
}
