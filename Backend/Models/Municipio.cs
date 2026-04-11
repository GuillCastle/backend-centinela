using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Backend.Models;

public partial class Municipio
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Departamento { get; set; }

    public string CodigoPostal { get; set; } = null!;

    public string? CodigoDpi { get; set; }

    public Geometry? PuntoGeografico { get; set; }
}
