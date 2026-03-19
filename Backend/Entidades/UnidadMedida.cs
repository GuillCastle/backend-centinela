using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class UnidadMedida
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Abreviar { get; set; } = null!;

    public int Estado { get; set; }

    public string Usuario { get; set; } = null!;

    public DateTime Fecha { get; set; }
}
