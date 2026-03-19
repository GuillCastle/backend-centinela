namespace Backend.DTOs.Municipio
{
    public class MunicipioIdDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Departamento { get; set; }

        public string CodigoPostal { get; set; } = null!;
    }
}
