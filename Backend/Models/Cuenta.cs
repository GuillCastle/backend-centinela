using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Cuenta
{
    public int Codigo { get; set; }

    public int? PadreCodigo { get; set; }

    public string CodigoNivel { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int Usuario { get; set; }

    public int Estado { get; set; }

    public int? NivelCuenta { get; set; }

    public virtual ICollection<Cuenta> InversePadreCodigoNavigation { get; set; } = new List<Cuenta>();

    public virtual Cuenta? PadreCodigoNavigation { get; set; }
}
