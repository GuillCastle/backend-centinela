using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Backend.Models;

public partial class Evento
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }

    public int Usuario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int AperturaCampanaElectoral { get; set; }

    public int? Departamento { get; set; }

    public int? Municipio { get; set; }

    public Geometry? PuntoGeografico { get; set; }

    public int? TipoEvento { get; set; }

    public virtual AperturaCampanaElectoral AperturaCampanaElectoralNavigation { get; set; } = null!;

    public virtual ICollection<DetalleEvento> DetalleEventos { get; set; } = new List<DetalleEvento>();

    public virtual TipoEvento? TipoEventoNavigation { get; set; }

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
