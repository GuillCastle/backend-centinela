using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class DetalleCompraServicio
{
    public long Codigo { get; set; }

    public long Compra { get; set; }

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime? PeriodoDesde { get; set; }

    public DateTime? PeriodoHasta { get; set; }

    public decimal Monto { get; set; }

    public decimal Iva { get; set; }

    public decimal TotalSinIva { get; set; }

    public virtual Compra CompraNavigation { get; set; } = null!;
}
