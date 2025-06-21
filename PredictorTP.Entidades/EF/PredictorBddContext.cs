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

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GEMELOS\\SQLEXPRESS;Database=PredictorBDD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usuario__CB9A1CFF0B201553");

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
