using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DetalleCompraProducto
{
    public long Producto { get; set; }

    public long Compra { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioCosto { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal Monto { get; set; }

    public int CantidadFaltante { get; set; }

    public decimal Iva { get; set; }

    public decimal TotalSinIva { get; set; }

    public virtual Compra CompraNavigation { get; set; } = null!;

    public virtual Producto ProductoNavigation { get; set; } = null!;
}
