using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PredictorTP.Entidades.EF;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    [Column("userId")]
    public int UserId { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [Column("tokenconfirmacion")]
    [StringLength(255)]
    public string? Tokenconfirmacion { get; set; }

    [Column("contrasenia")]
    [StringLength(255)]
    public string Contrasenia { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("verificado")]
    public bool Verificado { get; set; }

    [Column("administrador")]
    public bool Administrador { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<ResultadoPrediccion> ResultadoPrediccions { get; set; } = new List<ResultadoPrediccion>();
}
