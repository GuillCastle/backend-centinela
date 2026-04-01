using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class EncabezadoCuadrilla
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }

    public int UsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<DetalleUsuarioCuadrilla> DetalleUsuarioCuadrillas { get; set; } = new List<DetalleUsuarioCuadrilla>();
}
