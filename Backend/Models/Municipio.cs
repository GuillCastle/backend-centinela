using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Municipio
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Departamento { get; set; }

    public string CodigoPostal { get; set; } = null!;

    public string? CodigoDpi { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
