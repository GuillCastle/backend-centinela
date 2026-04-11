using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class TipoCuadrilla
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }

    public virtual ICollection<DetalleEvento> DetalleEventos { get; set; } = new List<DetalleEvento>();

    public virtual ICollection<DetalleUsuarioCuadrilla> DetalleUsuarioCuadrillas { get; set; } = new List<DetalleUsuarioCuadrilla>();
}
