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

    public virtual DbSet<DatoIdioma> DatoIdiomas { get; set; }

    public virtual DbSet<DatoPolaridad> DatoPolaridads { get; set; }

    public virtual DbSet<DatoSentimiento> DatoSentimientos { get; set; }

    public virtual DbSet<PersonaDetectadum> PersonaDetectada { get; set; }

    public virtual DbSet<ResultadoImagen> ResultadoImagens { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GEMELOS\\SQLEXPRESS;Database=PredictorBDD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatoIdioma>(entity =>
        {
            entity.HasKey(e => e.FraseIdiomaId).HasName("PK__DatoIdio__5523842341B2AEEC");

            entity.ToTable("DatoIdioma");

            entity.Property(e => e.FraseIdiomaId).HasColumnName("fraseIdiomaId");
            entity.Property(e => e.FraseEnIdioma).HasColumnName("fraseEnIdioma");
            entity.Property(e => e.Idioma)
                .HasMaxLength(50)
                .HasColumnName("idioma");
            entity.Property(e => e.PorcentajeDeConfianza).HasColumnName("porcentajeDeConfianza");
        });

        modelBuilder.Entity<DatoPolaridad>(entity =>
        {
            entity.HasKey(e => e.DatoPolaridadId).HasName("PK__DatoPola__A2F5753CF8533BC3");

            entity.ToTable("DatoPolaridad");

            entity.Property(e => e.DatoPolaridadId).HasColumnName("datoPolaridadId");
            entity.Property(e => e.ProbabilidadNegativa).HasColumnName("_probabilidadNegativa");
            entity.Property(e => e.ProbabilidadPositiva).HasColumnName("_probabilidadPositiva");
            entity.Property(e => e.Resutlado)
                .HasMaxLength(50)
                .HasColumnName("_resutlado");
            entity.Property(e => e.TextoProcesado).HasColumnName("_textoProcesado");
        });

        modelBuilder.Entity<DatoSentimiento>(entity =>
        {
            entity.HasKey(e => e.ResultadoId).HasName("PK__DatoSent__008B8537E9AF8BF6");

            entity.ToTable("DatoSentimiento");

            entity.Property(e => e.ResultadoId).HasColumnName("resultadoId");
            entity.Property(e => e.FraseConSentimiento).HasColumnName("fraseConSentimiento");
            entity.Property(e => e.PorcentajeDeConfianza).HasColumnName("porcentajeDeConfianza");
            entity.Property(e => e.Sentimiento)
                .HasMaxLength(100)
                .HasColumnName("sentimiento");
        });

        modelBuilder.Entity<PersonaDetectadum>(entity =>
        {
            entity.HasKey(e => e.PersonaDetectadaId).HasName("PK__PersonaD__5435198038CC080F");

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
            entity.HasKey(e => e.ResultadoImagenId).HasName("PK__Resultad__FEF8BEC9443F926F");

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
            entity.HasKey(e => e.UserId).HasName("PK__Usuario__CB9A1CFF5E6FDE47");

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
