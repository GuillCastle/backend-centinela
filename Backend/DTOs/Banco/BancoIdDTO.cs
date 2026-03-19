namespace Backend.DTOs.Banco
{
    public class BancoIdDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Usuario { get; set; }

        public int Estado { get; set; }
    }
}
