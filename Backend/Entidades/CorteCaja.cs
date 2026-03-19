using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class CorteCaja
{
    public long Codigo { get; set; }

    public DateTime FechaHoraApertura { get; set; }

    public DateTime? FechaHoraCierre { get; set; }

    public decimal SaldoInicial { get; set; }

    public decimal? SaldoFinal { get; set; }

    public int UsuarioResponsable { get; set; }

    public int? UsuarioVerificacion { get; set; }

    public int? Estado { get; set; }
}
