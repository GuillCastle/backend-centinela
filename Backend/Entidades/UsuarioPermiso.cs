using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class UsuarioPermiso
{
    public int Usuario { get; set; }

    public int Permiso { get; set; }

    public int Busqueda { get; set; }

    public int Insertar { get; set; }

    public int Reimpresion { get; set; }

    public virtual Permiso PermisoNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}