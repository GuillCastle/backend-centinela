namespace Backend.DTOs.Clientes
{
    public class ClientesDTO
    {
        public int Codigo { get; set; }

        public string? Nit { get; set; }

        public string? Dpi { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string Departamento { get; set; } = null!;

        public string Municipio { get; set; } = null!;

        public string Estado { get; set; } = null!;
    }
}
