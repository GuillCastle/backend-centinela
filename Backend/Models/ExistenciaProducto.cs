using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class ExistenciaProducto
{
    public long Producto { get; set; }

    public decimal PrecioCompra { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioVenta { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public int Año { get; set; }

    public int Usuario { get; set; }

    public DateTime FechaRegistro { get; set; }
}
