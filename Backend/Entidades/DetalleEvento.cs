using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DetalleEvento
{
    public long Codigo { get; set; }

    public int Evento { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Usuario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int TipoCuadrilla { get; set; }

    public virtual ICollection<DetalleEventoDocumento> DetalleEventoDocumentos { get; set; } = new List<DetalleEventoDocumento>();

    public virtual Evento EventoNavigation { get; set; } = null!;

    public virtual TipoCuadrilla TipoCuadrillaNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
