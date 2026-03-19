namespace Backend.DTOs.Banco
{
    public class BancoDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Usuario { get; set; }

        public string Estado { get; set; }
    }
}
