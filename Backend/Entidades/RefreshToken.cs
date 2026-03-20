using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string? Token { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public bool Revocado { get; set; }

    public DateTime FechaCreacion { get; set; }
}
