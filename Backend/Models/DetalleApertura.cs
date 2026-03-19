using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class DetalleApertura
{
    public long Producto { get; set; }

    public int Apertura { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioCosto { get; set; }

    public decimal PrecioVenta { get; set; }

    public virtual AperturaInventario AperturaNavigation { get; set; } = null!;

    public virtual Producto ProductoNavigation { get; set; } = null!;
}
