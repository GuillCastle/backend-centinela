using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DetalleCompraCombustible
{
    public int Combustible { get; set; }

    public long Compra { get; set; }

    public int Galones { get; set; }

    public decimal PrecioCosto { get; set; }

    public decimal Monto { get; set; }

    public decimal Iva { get; set; }

    public decimal TotalSinIva { get; set; }

    public decimal Exento { get; set; }

    public decimal TotalExento { get; set; }

    public virtual Combustible CombustibleNavigation { get; set; } = null!;

    public virtual Compra CompraNavigation { get; set; } = null!;
}
