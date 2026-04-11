using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Departamento
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Pais { get; set; }

    public Geometry? PuntosGeograficos { get; set; }

}
