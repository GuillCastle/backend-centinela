using System;
using System.Collections.Generic;
using Backend.Entidades;

using Microsoft.EntityFrameworkCore;

namespace Backend;

public partial class ApplicationDbContext : DbContext
{
    internal object proveedores;

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activo> Activos { get; set; }

    public virtual DbSet<AgentesEconomico> AgentesEconomicos { get; set; }

    public virtual DbSet<AperturaAnio> AperturaAnios { get; set; }

    public virtual DbSet<AperturaCreditoCompra> AperturaCreditoCompras { get; set; }

    public virtual DbSet<AperturaCreditoVentum> AperturaCreditoVenta { get; set; }

    public virtual DbSet<AperturaInventario> AperturaInventarios { get; set; }

    public virtual DbSet<Banco> Bancos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Combustible> Combustibles { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Concepto> Conceptos { get; set; }

    public virtual DbSet<CorteCaja> CorteCajas { get; set; }

    public virtual DbSet<CorteCajaMovimiento> CorteCajaMovimientos { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleApertura> DetalleAperturas { get; set; }

    public virtual DbSet<DetalleAperturaActivo> DetalleAperturaActivos { get; set; }

    public virtual DbSet<DetalleCompraActivo> DetalleCompraActivos { get; set; }

    public virtual DbSet<DetalleCompraCombustible> DetalleCompraCombustibles { get; set; }

    public virtual DbSet<DetalleCompraProducto> DetalleCompraProductos { get; set; }

    public virtual DbSet<DetalleCompraServicio> DetalleCompraServicios { get; set; }

    public virtual DbSet<DetalleVentaActivo> DetalleVentaActivos { get; set; }

    public virtual DbSet<DetalleVentaProducto> DetalleVentaProductos { get; set; }

    public virtual DbSet<DetalleVentaServicio> DetalleVentaServicios { get; set; }

    public virtual DbSet<DocumentoPorCobrar> DocumentoPorCobrars { get; set; }

    public virtual DbSet<DocumentoPorCobrarAbono> DocumentoPorCobrarAbonos { get; set; }

    public virtual DbSet<DocumentoPorPagar> DocumentoPorPagars { get; set; }

    public virtual DbSet<DocumentoPorPagarAbono> DocumentoPorPagarAbonos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<ExistenciaProducto> ExistenciaProductos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MovimientoProducto> MovimientoProductos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Presentacion> Presentacions { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProveedorCuentaBancarium> ProveedorCuentaBancaria { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Retencione> Retenciones { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolPermiso> RolPermisos { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }

    public virtual DbSet<TipoProveedor> TipoProveedors { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        //optionsBuilder.AddInterceptors(_interceptor);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Activo>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Activo");

            entity.Property(e => e.FechaCompra).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NoFactura).HasMaxLength(50);
            entity.Property(e => e.SerieFactura).HasMaxLength(50);
            entity.Property(e => e.TasaDepreciacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorActivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorLibros).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VidaUtil).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<AgentesEconomico>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Abreviacion).HasMaxLength(5);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
        });

        modelBuilder.Entity<AperturaAnio>(entity =>
        {
            entity.HasKey(e => e.Año).HasName("PK_AperturaAño");

            entity.ToTable("AperturaAnio");

            entity.Property(e => e.Año).ValueGeneratedNever();
            entity.Property(e => e.FechaApertura).HasColumnType("datetime");
            entity.Property(e => e.FechaCierre).HasColumnType("datetime");
            entity.Property(e => e.UsuarioApertura).HasMaxLength(50);
            entity.Property(e => e.UsuarioCierre).HasMaxLength(50);
        });

        modelBuilder.Entity<AperturaCreditoCompra>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("AperturaCreditoCompra");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AperturaNavigation).WithMany(p => p.AperturaCreditoCompras)
                .HasForeignKey(d => d.Apertura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AperturaCreditoCompra_AperturaInventario");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.AperturaCreditoCompras)
                .HasForeignKey(d => d.Proveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AperturaCreditoCompra_Proveedores");
        });

        modelBuilder.Entity<AperturaCreditoVentum>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AperturaNavigation).WithMany(p => p.AperturaCreditoVenta)
                .HasForeignKey(d => d.Apertura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AperturaCreditoVenta_AperturaInventario");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.AperturaCreditoVenta)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AperturaCreditoVenta_Clientes");
        });

        modelBuilder.Entity<AperturaInventario>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("AperturaInventario");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Usuario).HasMaxLength(50);

            entity.HasOne(d => d.EntidadNavigation).WithMany(p => p.AperturaInventarios)
                .HasForeignKey(d => d.Entidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AperturaInventario_Empresa");

            entity.HasOne(d => d.SucursalNavigation).WithMany(p => p.AperturaInventarios)
                .HasForeignKey(d => d.Sucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AperturaInventario_Sucursales");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Banco");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.CorreoElectronico).HasMaxLength(200);
            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .HasColumnName("DPI");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nit).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(13);

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Departamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Departamento");

            entity.HasOne(d => d.MunicipioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Municipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Municipio");

            entity.HasOne(d => d.PaisNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Pais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Pais");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Usuario");
        });

        modelBuilder.Entity<Combustible>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Combustible");

            entity.Property(e => e.Exento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Compra");

            entity.Property(e => e.Exento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.FacturaEspecial).HasDefaultValue(false);
            entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");
            entity.Property(e => e.FechaDocumento).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FechaRevision).HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
            entity.Property(e => e.RetencionIsr)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("RetencionISR");
            entity.Property(e => e.RetencionIva)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("RetencionIVA");
            entity.Property(e => e.SerieDocumento).HasMaxLength(50);
            entity.Property(e => e.TotalDocumento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalPagar).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalSinIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.UrlretencionIsr).HasColumnName("URLRetencionISR");
            entity.Property(e => e.UrlretencionIva).HasColumnName("URLRetencionIVA");
            entity.Property(e => e.Uuid)
                .HasMaxLength(70)
                .HasColumnName("UUID");

            entity.HasOne(d => d.EntidadNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Entidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compra_Empresa");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Proveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compra_Proveedores");

            entity.HasOne(d => d.SucursalNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Sucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compra_Sucursales");
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
        });

        modelBuilder.Entity<CorteCaja>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("CorteCaja");

            entity.Property(e => e.FechaHoraApertura).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraCierre).HasColumnType("datetime");
            entity.Property(e => e.SaldoFinal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<CorteCajaMovimiento>(entity =>
        {
            entity.HasKey(e => e.Movimiento);

            entity.ToTable("CorteCajaMovimiento");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.CodigoNivel).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(350);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.PadreCodigoNavigation).WithMany(p => p.InversePadreCodigoNavigation)
                .HasForeignKey(d => d.PadreCodigo)
                .HasConstraintName("FK_Cuentas_Cuentas");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Departamento");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<DetalleApertura>(entity =>
        {
            entity.HasKey(e => new { e.Producto, e.Apertura });

            entity.ToTable("DetalleApertura");

            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AperturaNavigation).WithMany(p => p.DetalleAperturas)
                .HasForeignKey(d => d.Apertura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleApertura_AperturaInventario");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.DetalleAperturas)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleApertura_Producto");
        });

        modelBuilder.Entity<DetalleAperturaActivo>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_AperturaActivo");

            entity.ToTable("DetalleAperturaActivo");

            entity.Property(e => e.FechaCompra).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NoFactura).HasMaxLength(50);
            entity.Property(e => e.SerieFactura).HasMaxLength(50);
            entity.Property(e => e.TasaDepreciacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorActivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorLibros).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VidaUtil).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AperturaNavigation).WithMany(p => p.DetalleAperturaActivos)
                .HasForeignKey(d => d.Apertura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleAperturaActivo_AperturaInventario");
        });

        modelBuilder.Entity<DetalleCompraActivo>(entity =>
        {
            entity.HasKey(e => new { e.Compra, e.Codigo });

            entity.ToTable("DetalleCompraActivo");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.TasaDepreciacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorActivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorLibros).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorSinIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.VidaUtil).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CompraNavigation).WithMany(p => p.DetalleCompraActivos)
                .HasForeignKey(d => d.Compra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraActivo_Compra");
        });

        modelBuilder.Entity<DetalleCompraCombustible>(entity =>
        {
            entity.HasKey(e => new { e.Combustible, e.Compra });

            entity.ToTable("DetalleCompraCombustible");

            entity.Property(e => e.Exento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalExento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalSinIva).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.CombustibleNavigation).WithMany(p => p.DetalleCompraCombustibles)
                .HasForeignKey(d => d.Combustible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraCombustible_Combustible");

            entity.HasOne(d => d.CompraNavigation).WithMany(p => p.DetalleCompraCombustibles)
                .HasForeignKey(d => d.Compra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraCombustible_Comrpa");
        });

        modelBuilder.Entity<DetalleCompraProducto>(entity =>
        {
            entity.HasKey(e => new { e.Producto, e.Compra });

            entity.ToTable("DetalleCompraProducto");

            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalSinIva).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.CompraNavigation).WithMany(p => p.DetalleCompraProductos)
                .HasForeignKey(d => d.Compra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraProducto_Comrpa");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.DetalleCompraProductos)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraProducto_Producto");
        });

        modelBuilder.Entity<DetalleCompraServicio>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.Compra });

            entity.ToTable("DetalleCompraServicio");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PeriodoDesde).HasColumnType("datetime");
            entity.Property(e => e.PeriodoHasta).HasColumnType("datetime");
            entity.Property(e => e.TotalSinIva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("TotalSinIVA");

            entity.HasOne(d => d.CompraNavigation).WithMany(p => p.DetalleCompraServicios)
                .HasForeignKey(d => d.Compra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraServicio_Comrpa");
        });

        modelBuilder.Entity<DetalleVentaActivo>(entity =>
        {
            entity.HasKey(e => new { e.Venta, e.Codigo });

            entity.ToTable("DetalleVentaActivo");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.TasaDepreciacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorActivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorLibros).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorSinIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.VidaUtil).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ActivoNavigation).WithMany(p => p.DetalleVentaActivos)
                .HasForeignKey(d => d.Activo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentaActivo_Activo");

            entity.HasOne(d => d.VentaNavigation).WithMany(p => p.DetalleVentaActivos)
                .HasForeignKey(d => d.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentaActivo_Venta");
        });

        modelBuilder.Entity<DetalleVentaProducto>(entity =>
        {
            entity.HasKey(e => new { e.Producto, e.Venta });

            entity.ToTable("DetalleVentaProducto");

            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalSinIva).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.DetalleVentaProductos)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentaProducto_Producto");

            entity.HasOne(d => d.VentaNavigation).WithMany(p => p.DetalleVentaProductos)
                .HasForeignKey(d => d.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentaProducto_Venta");
        });

        modelBuilder.Entity<DetalleVentaServicio>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.Venta });

            entity.ToTable("DetalleVentaServicio");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PeriodoDesde).HasColumnType("datetime");
            entity.Property(e => e.PeriodoHasta).HasColumnType("datetime");
            entity.Property(e => e.TotalSinIva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("TotalSinIVA");

            entity.HasOne(d => d.VentaNavigation).WithMany(p => p.DetalleVentaServicios)
                .HasForeignKey(d => d.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompraServicio_Venta");
        });

        modelBuilder.Entity<DocumentoPorCobrar>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("DocumentoPorCobrar");

            entity.Property(e => e.Estado).HasComment("0 pendiente/ 1 parcial/ 2 pagado/ 9 cuenta incobrable");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.MontoDocumento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.MontoPagado).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.SaldoPendiente).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.DocumentoPorCobrars)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentoPorCobrar_Clientes");
        });

        modelBuilder.Entity<DocumentoPorCobrarAbono>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_AbonoDocumentoPorCobrar");

            entity.ToTable("DocumentoPorCobrarAbono");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.DocumentoPorCobrarNavigation).WithMany(p => p.DocumentoPorCobrarAbonos)
                .HasForeignKey(d => d.DocumentoPorCobrar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentoPorCobrarAbono_DocumentoPorCobrar");
        });

        modelBuilder.Entity<DocumentoPorPagar>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("DocumentoPorPagar");

            entity.Property(e => e.Estado).HasComment("0 pendiente/ 1 parcial/ 2 pagado/ 9 cuenta incobrable");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.MontoDocumento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.MontoPagado).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.SaldoPendiente).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.DocumentoPorPagars)
                .HasForeignKey(d => d.Proveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentoPorPagar_Proveedores");
        });

        modelBuilder.Entity<DocumentoPorPagarAbono>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_AbonoDocumentoPorPagar");

            entity.ToTable("DocumentoPorPagarAbono");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.DocumentoPorPagarNavigation).WithMany(p => p.DocumentoPorPagarAbonos)
                .HasForeignKey(d => d.DocumentoPorPagar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentoPorPagarAbono_DocumentoPorPagar");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Empresa");

            entity.Property(e => e.CodigoEmpresa).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Representante).HasMaxLength(250);
        });

        modelBuilder.Entity<ExistenciaProducto>(entity =>
        {
            entity.HasKey(e => new { e.Producto, e.Entidad, e.Sucursal });

            entity.ToTable("ExistenciaProducto");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.PrecioCompra).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Marca");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
        });

        modelBuilder.Entity<MovimientoProducto>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("MovimientoProducto");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.IdEntrada).HasColumnName("Id_entrada");
            entity.Property(e => e.IdSalida).HasColumnName("Id_salida");
            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.MovimientoProductos)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoProducto_Producto");

            entity.HasOne(d => d.TipoMovimientoNavigation).WithMany(p => p.MovimientoProductos)
                .HasForeignKey(d => d.TipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoProducto_TipoMovimiento");
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

        modelBuilder.Entity<Presentacion>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Presentacion");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Usuario).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Producto");

            entity.Property(e => e.CodBarras).HasMaxLength(50);
            entity.Property(e => e.CodBusqueda).HasMaxLength(50);
            entity.Property(e => e.DescripcionSat)
                .HasMaxLength(50)
                .HasColumnName("DescripcionSAT");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Usuario).HasMaxLength(50);

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Marca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Marca");

            entity.HasOne(d => d.UnidadMedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.UnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_UnidadMedida");
        });

        modelBuilder.Entity<ProveedorCuentaBancarium>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Cuenta).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.ProveedorCuentaBancaria)
                .HasForeignKey(d => d.Proveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProveedorCuentaBancaria_Proveedores");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.CorreoElectronico).HasMaxLength(200);
            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .HasColumnName("DPI");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nit).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(13);

            entity.HasOne(d => d.ConceptosNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.Conceptos)
                .HasConstraintName("FK_Proveedores_Conceptos");

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.Departamento)
                .HasConstraintName("FK_Proveedores_Departamento");

            entity.HasOne(d => d.MunicipioNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.Municipio)
                .HasConstraintName("FK_Proveedores_Municipio");

            entity.HasOne(d => d.PaisNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.Pais)
                .HasConstraintName("FK_Proveedores_Pais");

            entity.HasOne(d => d.RetencionNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.Retencion)
                .HasConstraintName("FK_Proveedores_Retenciones");

            entity.HasOne(d => d.TipoProveedorNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.TipoProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedores_TipoProveedor");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedores_Usuario");
        });

        modelBuilder.Entity<Retencione>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Descripcion).HasMaxLength(200);
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

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("TipoMovimiento");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoProveedor>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("TipoProveedor");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Abreviar).HasMaxLength(3);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Usuario).HasMaxLength(50);
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

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.FechaCaja).HasColumnType("datetime");
            entity.Property(e => e.FechaDocumento).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("IVA");
            entity.Property(e => e.NombreConsumidorFinal).HasMaxLength(1000);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
            entity.Property(e => e.SerieDocumento).HasMaxLength(50);
            entity.Property(e => e.TotalDocumento).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalSinIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.UsuarioCaja).HasColumnName("UsuarioCAja");
            entity.Property(e => e.Uuid)
                .HasMaxLength(70)
                .HasColumnName("UUID");
            entity.Property(e => e.Xml).HasColumnName("XML");
        });

        OnModelCreatingPartial(modelBuilder);

    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
