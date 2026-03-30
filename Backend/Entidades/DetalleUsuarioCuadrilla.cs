using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DetalleUsuarioCuadrilla
{
    public int Codigo { get; set; }

    public int Cuadrilla { get; set; }

    public int Usuario { get; set; }

    public int Estado { get; set; }

    public int UsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int TipoCuadrilla { get; set; }

    public virtual EncabezadoCuadrilla CuadrillaNavigation { get; set; } = null!;

    public virtual TipoCuadrilla TipoCuadrillaNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
