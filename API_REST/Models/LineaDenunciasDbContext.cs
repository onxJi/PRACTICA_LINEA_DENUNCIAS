using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Models;

public partial class LineaDenunciasDbContext : DbContext
{
    public LineaDenunciasDbContext()
    {
    }

    public LineaDenunciasDbContext(DbContextOptions<LineaDenunciasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    public virtual DbSet<Centro> Centros { get; set; }

    public virtual DbSet<DatosPersonal> DatosPersonales { get; set; }

    public virtual DbSet<Denuncia> Denuncias { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<HistorialDenuncia> HistorialDenuncia { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<UserAdmin> UserAdmins { get; set; }

    public virtual DbSet<UserDenunciante> UserDenunciantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalogo>(entity =>
        {
            entity.HasKey(e => e.IdDenuncia).HasName("PK__Catalogo__872A6BC450104EF4");

            entity.ToTable("Catalogo");

            entity.Property(e => e.IdDenuncia)
                .ValueGeneratedNever()
                .HasColumnName("ID_Denuncia");
            entity.Property(e => e.Empresa).IsUnicode(false);
            entity.Property(e => e.Estado).IsUnicode(false);
            entity.Property(e => e.Pais).IsUnicode(false);
        });

        modelBuilder.Entity<Centro>(entity =>
        {
            entity.HasKey(e => e.IdCentro).HasName("PK__Centro__F0BF374E93817767");

            entity.ToTable("Centro");

            entity.Property(e => e.IdCentro).HasColumnName("ID_Centro");
            entity.Property(e => e.NumCentro).IsUnicode(false);
        });

        modelBuilder.Entity<DatosPersonal>(entity =>
        {
            entity.HasKey(e => e.IdDenunciante).HasName("PK__DatosPer__1BF8559AACF4E3C6");

            entity.Property(e => e.IdDenunciante).HasColumnName("ID_Denunciante");
            entity.Property(e => e.Correo).IsUnicode(false);
            entity.Property(e => e.IdDenuncia).HasColumnName("ID_Denuncia");
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.Telefono).IsUnicode(false);
        });

        modelBuilder.Entity<Denuncia>(entity =>
        {
            entity.HasKey(e => e.Folio).HasName("PK__Denuncia__BAB84EF66E038BA2");

            entity.Property(e => e.Folio).ValueGeneratedNever();
            entity.Property(e => e.DetalleDenuncia).IsUnicode(false);
            entity.Property(e => e.Empresa).IsUnicode(false);
            entity.Property(e => e.Estado).IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.FechaDenuncia).HasColumnType("datetime");
            entity.Property(e => e.IdDenuncia).HasColumnName("ID_Denuncia");
            entity.Property(e => e.IdDenunciante).HasColumnName("ID_Denunciante");
            entity.Property(e => e.Pais).IsUnicode(false);
            entity.Property(e => e.PasswordDenuncia).IsUnicode(false);
            entity.Property(e => e.TituloDenuncia).IsUnicode(false);
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresas).HasName("PK__Empresa__5B27FFB88FA31D0E");

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmpresas).HasColumnName("ID_Empresas");
            entity.Property(e => e.Empresas).IsUnicode(false);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstados).HasName("PK__Estado__F698A44D388CB3D2");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstados).HasColumnName("ID_Estados");
            entity.Property(e => e.Estados).IsUnicode(false);
        });

        modelBuilder.Entity<HistorialDenuncia>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__ECA894540C780584");

            entity.Property(e => e.IdHistorial).HasColumnName("ID_Historial");
            entity.Property(e => e.Historial).IsUnicode(false);
            entity.Property(e => e.IdDenuncia).HasColumnName("ID_Denuncia");
            entity.Property(e => e.IdDenunciante).HasColumnName("ID_Denunciante");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.IdPaises).HasName("PK__Pais__CC3C5A99887F870E");

            entity.Property(e => e.IdPaises).HasColumnName("ID_Paises");
            entity.Property(e => e.Paises).IsUnicode(false);
        });

        modelBuilder.Entity<UserAdmin>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__UserAdmi__DE4431C5CF945946");

            entity.ToTable("UserAdmin");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Pass).IsUnicode(false);
            entity.Property(e => e.Usuario).IsUnicode(false);
        });

        modelBuilder.Entity<UserDenunciante>(entity =>
        {
            entity.HasKey(e => e.IdDenunciante).HasName("PK__UserDenu__1BF8559AAB441294");

            entity.ToTable("UserDenunciante");

            entity.Property(e => e.IdDenunciante).HasColumnName("ID_Denunciante");
            entity.Property(e => e.PasswordDenuncia).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
