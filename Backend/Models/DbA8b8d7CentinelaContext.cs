using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class DbA8b8d7CentinelaContext : DbContext
{
    public DbA8b8d7CentinelaContext()
    {
    }

    public DbA8b8d7CentinelaContext(DbContextOptions<DbA8b8d7CentinelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AperturaCampanaElectoral> AperturaCampanaElectorals { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleUsuarioCuadrilla> DetalleUsuarioCuadrillas { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EncabezadoCuadrilla> EncabezadoCuadrillas { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolPermiso> RolPermisos { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<TipoCuadrilla> TipoCuadrillas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL5105.site4now.net;Initial Catalog=db_a8b8d7_centinela;User Id=db_a8b8d7_centinela_admin;Password=JayceViktor1301@.;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AperturaCampanaElectoral>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("AperturaCampanaElectoral");

            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Departamento");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<DetalleUsuarioCuadrilla>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("DetalleUsuarioCuadrilla");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.CuadrillaNavigation).WithMany(p => p.DetalleUsuarioCuadrillas)
                .HasForeignKey(d => d.Cuadrilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleUsuarioCuadrilla_EncabezadoCuadrilla");

            entity.HasOne(d => d.TipoCuadrillaNavigation).WithMany(p => p.DetalleUsuarioCuadrillas)
                .HasForeignKey(d => d.TipoCuadrilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleUsuarioCuadrilla_TipoCuadrilla");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.DetalleUsuarioCuadrillas)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleUsuarioCuadrilla_Usuario");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Empresa");

            entity.Property(e => e.CodigoEmpresa).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Representante).HasMaxLength(250);
        });

        modelBuilder.Entity<EncabezadoCuadrilla>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("EncabezadoCuadrilla");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Municipio");

            entity.Property(e => e.CodigoDpi)
                .HasMaxLength(4)
                .HasColumnName("CodigoDPI");
            entity.Property(e => e.CodigoPostal).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.IdentificadorPais).HasMaxLength(5);
            entity.Property(e => e.Nacionalidad).HasMaxLength(50);
            entity.Property(e => e.Pais).HasMaxLength(50);
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Permiso");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Rol");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<RolPermiso>(entity =>
        {
            entity.HasKey(e => new { e.Rol, e.Permiso });

            entity.ToTable("RolPermiso");

            entity.HasOne(d => d.PermisoNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.Permiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolPermiso_Permiso");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolPermiso_Rol");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.HasIndex(e => e.EmpresaCodigo, "IX_Sucursales_EmpresaCodigo");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Direccion)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Encargado).HasMaxLength(250);
            entity.Property(e => e.LlaveSucursal).HasColumnName("LLaveSucursal");

            entity.HasOne(d => d.EmpresaCodigoNavigation).WithMany(p => p.Sucursales).HasForeignKey(d => d.EmpresaCodigo);
        });

        modelBuilder.Entity<TipoCuadrilla>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("TipoCuadrilla");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NombreUsuario, "IX_Usuario").IsUnique();

            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(50);
            entity.Property(e => e.FechaRegistro).HasMaxLength(50);
            entity.Property(e => e.Foto).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.Salt).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

            entity.HasOne(d => d.EntidadNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Entidad)
                .HasConstraintName("FK_Usuario_Empresa");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");

            entity.HasOne(d => d.SucursalNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Sucursal)
                .HasConstraintName("FK_Usuario_Sucursales");
        });

        modelBuilder.Entity<UsuarioPermiso>(entity =>
        {
            entity.HasKey(e => new { e.Usuario, e.Permiso });

            entity.ToTable("UsuarioPermiso");

            entity.HasOne(d => d.PermisoNavigation).WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(d => d.Permiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioPermiso_Permiso");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioPermiso_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
