using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PredictorTP.Entidades.EF;

public partial class PredictorBddContext : DbContext
{
    public PredictorBddContext()
    {
    }

    public PredictorBddContext(DbContextOptions<PredictorBddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ResultadoPrediccion> ResultadoPrediccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-I7KQ4F1H;Database=PredictorBDD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResultadoPrediccion>(entity =>
        {
            entity.HasKey(e => e.ResultadoPrediccionId).HasName("PK__Resultad__FFAB39E8DB01524D");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ResultadoPrediccions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__Usuar__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usuario__CB9A1CFF1CB1BAD8");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
