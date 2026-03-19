using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class AperturaAnio
{
    public int Año { get; set; }

    public DateTime? FechaApertura { get; set; }

    public string? UsuarioApertura { get; set; }

    public DateTime? FechaCierre { get; set; }

    public string? UsuarioCierre { get; set; }
}
