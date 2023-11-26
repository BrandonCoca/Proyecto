using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Comentarios
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Autor { get; set; }
        [Required]
        public string? ContenidoComentario { get; set; }
        [Required]
        public DateTime FechaComentario { get; set; }
    }
}
