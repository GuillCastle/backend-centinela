using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class DetalleVentaProducto
{
    public long Producto { get; set; }

    public long Venta { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioCosto { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal Monto { get; set; }

    public decimal Iva { get; set; }

    public decimal TotalSinIva { get; set; }

    public virtual Producto ProductoNavigation { get; set; } = null!;

    public virtual Ventum VentaNavigation { get; set; } = null!;
}
