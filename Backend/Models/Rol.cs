using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Rol
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public int Estado { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
