
using Backend.Entidades;

namespace Backend.DTOs.Evento
{
    public class CreacionEventoGeneralDTO
    {
        public CreacionEventoDTO EncabezadoEvento { get; set; }
        public CreacionDetalleEventoDTO DetalleEvento { get; set; }
        public int Departamento { get; set; }
        public int Municipio { get; set; }
        public int TipoEvento { get; set; }
        public List<IFormFile> Documentos { get; set; } = new();
    }
}
