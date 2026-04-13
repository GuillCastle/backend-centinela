using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Notificacion
{
    public long Codigo { get; set; }

    public int Usuario { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int Evento { get; set; }

    public int Leida { get; set; }

    public int Estado { get; set; }

    public DateTime FechaRegistro { get; set; }
}
