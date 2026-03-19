using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class UnidadMedidum
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Abreviar { get; set; } = null!;

    public int Estado { get; set; }

    public string Usuario { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
