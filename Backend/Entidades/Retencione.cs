using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Retencione
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }

    public int Usuario { get; set; }

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
