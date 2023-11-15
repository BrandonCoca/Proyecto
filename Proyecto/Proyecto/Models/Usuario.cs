using Proyecto.Dtos;

namespace Proyecto.Models
{
    public class Usuario
    {
        public string? Cuenta { get; set; }
        public string? Nombre { get; set; }
        public string? Password { get; set; }
        public RolEnum Rol { get; set; }
    }
}
