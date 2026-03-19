using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class Combustible
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Exento { get; set; }

    public bool Estado { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public int UsuarioRegistro { get; set; }

    public virtual ICollection<DetalleCompraCombustible> DetalleCompraCombustibles { get; set; } = new List<DetalleCompraCombustible>();
}
