using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DetalleEventoDocumento
{
    public long Codigo { get; set; }

    public long DetalleEvento { get; set; }

    public string Url { get; set; } = null!;

    public int Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual DetalleEvento DetalleEventoNavigation { get; set; } = null!;
}
