using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class AperturaCreditoVentum
{
    public long Codigo { get; set; }

    public int Cliente { get; set; }

    public DateTime Fecha { get; set; }

    public string Motivo { get; set; } = null!;

    public decimal Monto { get; set; }

    public int Usuario { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public int Estado { get; set; }

    public int Apertura { get; set; }

    public virtual AperturaInventario AperturaNavigation { get; set; } = null!;

    public virtual Cliente ClienteNavigation { get; set; } = null!;
}
