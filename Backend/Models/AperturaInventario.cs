using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class AperturaInventario
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Usuario { get; set; } = null!;

    public string? Documento { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public int Estado { get; set; }

    public int? Anio { get; set; }

    public virtual ICollection<AperturaCreditoCompra> AperturaCreditoCompras { get; set; } = new List<AperturaCreditoCompra>();

    public virtual ICollection<AperturaCreditoVentum> AperturaCreditoVenta { get; set; } = new List<AperturaCreditoVentum>();

    public virtual ICollection<DetalleAperturaActivo> DetalleAperturaActivos { get; set; } = new List<DetalleAperturaActivo>();

    public virtual ICollection<DetalleApertura> DetalleAperturas { get; set; } = new List<DetalleApertura>();

    public virtual Empresa EntidadNavigation { get; set; } = null!;

    public virtual Sucursale SucursalNavigation { get; set; } = null!;
}
