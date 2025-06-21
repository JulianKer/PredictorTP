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

    public virtual DbSet<PersonaDetectadum> PersonaDetectada { get; set; }

    public virtual DbSet<ResultadoImagen> ResultadoImagens { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GERMAN\\SQLEXPRESS;Database=PredictorBDD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonaDetectadum>(entity =>
        {
            entity.HasKey(e => e.PersonaDetectadaId).HasName("PK__PersonaD__54351980A1E822F7");

            entity.Property(e => e.PersonaDetectadaId).HasColumnName("personaDetectadaId");
            entity.Property(e => e.DescripcionPersona)
                .HasMaxLength(100)
                .HasColumnName("descripcionPersona");
            entity.Property(e => e.ResultadoImagenId).HasColumnName("resultadoImagenId");

            entity.HasOne(d => d.ResultadoImagen).WithMany(p => p.PersonaDetectada)
                .HasForeignKey(d => d.ResultadoImagenId)
                .HasConstraintName("FK__PersonaDe__resul__5FB337D6");
        });

        modelBuilder.Entity<ResultadoImagen>(entity =>
        {
            entity.HasKey(e => e.ResultadoImagenId).HasName("PK__Resultad__FEF8BEC96CD409EE");

            entity.ToTable("ResultadoImagen");

            entity.Property(e => e.ResultadoImagenId).HasColumnName("resultadoImagenId");
            entity.Property(e => e.RutaImg)
                .HasMaxLength(255)
                .HasColumnName("rutaImg");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ResultadoImagens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__userI__5CD6CB2B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usuario__CB9A1CFF84158EED");

            entity.ToTable("Usuario");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Administrador).HasColumnName("administrador");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(255)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Tokenconfirmacion)
                .HasMaxLength(255)
                .HasColumnName("tokenconfirmacion");
            entity.Property(e => e.Verificado).HasColumnName("verificado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
