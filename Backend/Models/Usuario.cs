using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Usuario
{
    public int Codigo { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public int Estado { get; set; }

    public int Rol { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public string FechaRegistro { get; set; } = null!;

    public string? Foto { get; set; }

    public int? Entidad { get; set; }

    public int? Sucursal { get; set; }

    public virtual Empresa? EntidadNavigation { get; set; }

    public virtual Rol RolNavigation { get; set; } = null!;

    public virtual Sucursale? SucursalNavigation { get; set; }

    public virtual ICollection<UsuarioPermiso> UsuarioPermisos { get; set; } = new List<UsuarioPermiso>();
}
