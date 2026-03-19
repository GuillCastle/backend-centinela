using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Departamento
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Pais { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
