using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class ProveedorCuentaBancarium
{
    public int Codigo { get; set; }

    public int Proveedor { get; set; }

    public int Banco { get; set; }

    public string Cuenta { get; set; } = null!;

    public int Principal { get; set; }

    public int Usuario { get; set; }

    public DateTime Fecha { get; set; }

    public int Estado { get; set; }

    public virtual Proveedore ProveedorNavigation { get; set; } = null!;
}
