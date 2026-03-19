namespace Backend.DTOs.CuentaBancariaProveedor
{
    public class CuentasBanProveedoresidDTO
    {
        public int Codigo { get; set; }
        public int Proveedor { get; set; }
        public int Banco { get; set; } 
        public string Cuenta { get; set; } = null!;
        public int Principal { get; set; }
        public int Estado { get; set; }
        
 
    }
}
