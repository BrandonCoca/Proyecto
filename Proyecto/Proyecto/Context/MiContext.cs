using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Context
{
    public class MiContext:DbContext
    {
        public MiContext(DbContextOptions   options): base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Noticias> Noticias { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }

    }
}
