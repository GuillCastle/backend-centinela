using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class TipoMovimiento
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<MovimientoProducto> MovimientoProductos { get; set; } = new List<MovimientoProducto>();
}
