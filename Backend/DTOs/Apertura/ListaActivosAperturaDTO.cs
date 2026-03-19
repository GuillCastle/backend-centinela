namespace Backend.DTOs.Apertura
{
    public class ListaActivosAperturaDTO
    {
        public int Apertura { get; set; }

        public int Codigo { get; set; }

        public DateTime FechaCompra { get; set; }

        public string NoFactura { get; set; } = null!;

        public string SerieFactura { get; set; } = null!;

        public string DescripcionBien { get; set; } = null!;

        public decimal TasaDepreciacion { get; set; }

        public decimal ValorActivo { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal VidaUtil { get; set; }

        public decimal ValorLibros { get; set; }

    }
}
