using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PredictorTP.EF;

public partial class PredictorBddContext : DbContext
{
    public PredictorBddContext()
    {
    }

    public PredictorBddContext(DbContextOptions<PredictorBddContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-BNM5M7GL;Database=PredictorBDD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
