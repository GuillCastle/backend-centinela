using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class AperturaCampanaElectoral
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public int Estado { get; set; }

    public int Usuario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
