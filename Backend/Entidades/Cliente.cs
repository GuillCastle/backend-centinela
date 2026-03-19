using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Cliente
{
    public int Codigo { get; set; }

    public string? Nit { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public int Usuario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int Estado { get; set; }

    public int Pais { get; set; }

    public int Departamento { get; set; }

    public int Municipio { get; set; }

    public string? Dpi { get; set; }

    public virtual ICollection<AperturaCreditoVentum> AperturaCreditoVenta { get; set; } = new List<AperturaCreditoVentum>();

    public virtual Departamento DepartamentoNavigation { get; set; } = null!;

    public virtual ICollection<DocumentoPorCobrar> DocumentoPorCobrars { get; set; } = new List<DocumentoPorCobrar>();

    public virtual Municipio MunicipioNavigation { get; set; } = null!;

    public virtual Pai PaisNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
