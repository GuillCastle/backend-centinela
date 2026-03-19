using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Banco
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Usuario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int Estado { get; set; }
}
