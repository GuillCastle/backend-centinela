using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Compra
{
    public long Codigo { get; set; }

    public int Proveedor { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string SerieDocumento { get; set; } = null!;

    public DateTime FechaDocumento { get; set; }

    public string? Uuid { get; set; }

    public decimal TotalDocumento { get; set; }

    public decimal? RetencionIva { get; set; }

    public string? UrlretencionIva { get; set; }

    public decimal? RetencionIsr { get; set; }

    public string? UrlretencionIsr { get; set; }

    public decimal TotalPagar { get; set; }

    public decimal Iva { get; set; }

    public decimal Exento { get; set; }

    public decimal TotalSinIva { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int UsuarioRegistro { get; set; }

    public DateTime? FechaAprobacion { get; set; }

    public int? UsuarioAprobacion { get; set; }

    public DateTime? FechaRevision { get; set; }

    public int? UsuarioRevision { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public int Estado { get; set; }

    public int? Anio { get; set; }

    public bool? FacturaEspecial { get; set; }

    public string? DescripcionFaltante { get; set; }

    public virtual ICollection<DetalleCompraActivo> DetalleCompraActivos { get; set; } = new List<DetalleCompraActivo>();

    public virtual ICollection<DetalleCompraCombustible> DetalleCompraCombustibles { get; set; } = new List<DetalleCompraCombustible>();

    public virtual ICollection<DetalleCompraProducto> DetalleCompraProductos { get; set; } = new List<DetalleCompraProducto>();

    public virtual ICollection<DetalleCompraServicio> DetalleCompraServicios { get; set; } = new List<DetalleCompraServicio>();

    public virtual Empresa EntidadNavigation { get; set; } = null!;

    public virtual Proveedore ProveedorNavigation { get; set; } = null!;

    public virtual Sucursale SucursalNavigation { get; set; } = null!;
}
