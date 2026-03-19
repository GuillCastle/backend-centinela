namespace Backend.DTOs.CuentaBancariaProveedor
{
    public class CuentasBanProveedoreDTO
    {
        public int Codigo { get; set; }
        public string? Proveedor { get; set; }
        public string Banco { get; set; } = null!;
        public string Cuenta { get; set; } = null!;
        public string Principal { get; set; } = null!;
        public string Estado { get; set; } = null!;
       
       

    }
}
