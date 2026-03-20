using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Empresa
{
    public int Codigo { get; set; }

    public string CodigoEmpresa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Nit { get; set; }

    public string Representante { get; set; } = null!;

    public string Regimen { get; set; } = null!;

    public string AfiliacionIva { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? Usuario { get; set; }

    public string? Alias { get; set; }

    public int? Nivel { get; set; }

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

