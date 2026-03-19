using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Pai
{
    public int Codigo { get; set; }

    public string IdentificadorPais { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public int Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
