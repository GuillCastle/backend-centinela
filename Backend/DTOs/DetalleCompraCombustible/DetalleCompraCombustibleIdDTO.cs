namespace Backend.DTOs.DetalleCompraCombustible
{
    public class DetalleCompraCombustibleIdDTO
    {
        public int Combustible { get; set; }

        public long Compra { get; set; }

        public int Galones { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal Monto { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }

        public decimal Exento { get; set; }

        public decimal TotalExento { get; set; }
    }
}
