using System;
using System.Collections.Generic;

namespace Backend.Entidades;

public partial class DocumentoPorPagar
{
    public long Codigo { get; set; }

    public int TipoMovimiento { get; set; }

    public int Referencia { get; set; }

    public DateOnly FechaDocumento { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public decimal MontoDocumento { get; set; }

    public decimal MontoPagado { get; set; }

    public decimal SaldoPendiente { get; set; }

    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// 0 pendiente/ 1 parcial/ 2 pagado/ 9 cuenta incobrable
    /// </summary>
    public int Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int Usuario { get; set; }

    public int CuentaContable { get; set; }

    public int Entidad { get; set; }

    public int Sucursal { get; set; }

    public int Proveedor { get; set; }

    public virtual ICollection<DocumentoPorPagarAbono> DocumentoPorPagarAbonos { get; set; } = new List<DocumentoPorPagarAbono>();

    public virtual Proveedore ProveedorNavigation { get; set; } = null!;
}
