using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Backend.Models;

public partial class Departamento
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Pais { get; set; }

    public Geometry? PuntosGeograficos { get; set; }
}
