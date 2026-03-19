using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class AgentesEconomico
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Abreviacion { get; set; } = null!;

    public int Estado { get; set; }

    public DateTime Fecha { get; set; }
}
