using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class DocumentoPorPagarAbono
{
    public long Codigo { get; set; }

    public long DocumentoPorPagar { get; set; }

    public decimal Monto { get; set; }

    public int MetodoPago { get; set; }

    public long ReferenciaPago { get; set; }

    public string Observaciones { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int Usuario { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public virtual DocumentoPorPagar DocumentoPorPagarNavigation { get; set; } = null!;
}
