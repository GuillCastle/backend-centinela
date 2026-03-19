namespace Backend.DTOs.Clientes
{
    public class ClientesIdDTO
    {
        public int Codigo { get; set; }

        public string? Nit { get; set; }

        public string? Dpi { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public int Departamento { get; set; }

        public int Municipio { get; set; }

        public int Estado { get; set; }
    }
}
