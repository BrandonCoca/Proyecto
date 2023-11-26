using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Noticias
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ContenidoNoticia { get; set; }
        public string? Dedicatoria { get; set; }
        [Required]
        public DateTime FechaPublicado { get; set; }
        [Required]
        public string? Imagen { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [NotMapped]
        [Display(Name = "Cargar Foto")]
        public IFormFile? FotoFile { get; set; }
    }
}
