using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DocumentoPorCobrarAbono
{
    public long Codigo { get; set; }

    public long DocumentoPorCobrar { get; set; }

    public decimal Monto { get; set; }

    public int MetodoPago { get; set; }

    public long ReferenciaPago { get; set; }

    public string Observaciones { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int Usuario { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public virtual DocumentoPorCobrar DocumentoPorCobrarNavigation { get; set; } = null!;
}
