using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class CorteCajaMovimiento
{
    public long Movimiento { get; set; }

    public long CorteCaja { get; set; }

    public long CodigoReferencia { get; set; }

    public int TipoMovimiento { get; set; }

    public decimal Monto { get; set; }

    public int MetodoPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }
}
