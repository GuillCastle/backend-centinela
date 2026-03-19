using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Proveedore
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

    public int? Retencion { get; set; }

    public int? Conceptos { get; set; }

    public int? PequeñoContribuyente { get; set; }

    public int? RetenedorIva { get; set; }

    public int? Pais { get; set; }

    public int? Departamento { get; set; }

    public int? Municipio { get; set; }

    public int TipoProveedor { get; set; }

    public string? Dpi { get; set; }

    public virtual ICollection<AperturaCreditoCompra> AperturaCreditoCompras { get; set; } = new List<AperturaCreditoCompra>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Concepto? ConceptosNavigation { get; set; }

    public virtual Departamento? DepartamentoNavigation { get; set; }

    public virtual ICollection<DocumentoPorPagar> DocumentoPorPagars { get; set; } = new List<DocumentoPorPagar>();

    public virtual Municipio? MunicipioNavigation { get; set; }

    public virtual Pai? PaisNavigation { get; set; }

    public virtual ICollection<ProveedorCuentaBancarium> ProveedorCuentaBancaria { get; set; } = new List<ProveedorCuentaBancarium>();

    public virtual Retencione? RetencionNavigation { get; set; }

    public virtual TipoProveedor TipoProveedorNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
