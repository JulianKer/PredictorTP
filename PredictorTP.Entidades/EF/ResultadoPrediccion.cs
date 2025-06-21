using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PredictorTP.Entidades.EF;

[Table("ResultadoPrediccion")]
public partial class ResultadoPrediccion
{
    [Key]
    public int ResultadoPrediccionId { get; set; }

    public string Texto { get; set; } = null!;

    [StringLength(100)]
    public string TipoModelo { get; set; } = null!;

    [StringLength(255)]
    public string Prediccion { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Confianza { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("ResultadoPrediccions")]
    public virtual Usuario Usuario { get; set; } = null!;
}
