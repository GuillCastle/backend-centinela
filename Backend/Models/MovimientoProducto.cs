using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class MovimientoProducto
{
    public long Codigo { get; set; }

    public long Producto { get; set; }

    public int? IdEntrada { get; set; }

    public int TipoMovimiento { get; set; }

    public int? IdSalida { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioCosto { get; set; }

    public decimal Total { get; set; }

    public int Saldo { get; set; }

    public int SaldoTotal { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public virtual Producto ProductoNavigation { get; set; } = null!;

    public virtual TipoMovimiento TipoMovimientoNavigation { get; set; } = null!;
}
