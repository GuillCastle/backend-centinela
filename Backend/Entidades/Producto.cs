
using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Producto
{
    public long Codigo { get; set; }

    public string CodBusqueda { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Marca { get; set; }

    public int UnidadMedida { get; set; }

    public string Usuario { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int Estado { get; set; }

    public int AgenteEconomico { get; set; }

    public string? CodBarras { get; set; }

    public string? DescripcionSat { get; set; }

    public virtual ICollection<DetalleApertura> DetalleAperturas { get; set; } = new List<DetalleApertura>();

    public virtual ICollection<DetalleCompraProducto> DetalleCompraProductos { get; set; } = new List<DetalleCompraProducto>();

    public virtual ICollection<DetalleVentaProducto> DetalleVentaProductos { get; set; } = new List<DetalleVentaProducto>();

    public virtual Marca MarcaNavigation { get; set; } = null!;

    public virtual ICollection<MovimientoProducto> MovimientoProductos { get; set; } = new List<MovimientoProducto>();

    public virtual UnidadMedidum UnidadMedidaNavigation { get; set; } = null!;
}
