using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using VeterinaryApi.Models;

namespace VeterinaryApi.Context
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public Microsoft.EntityFrameworkCore.DbSet<Usuario> Usuarios { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AtencionServicio> atenciones_y_servicios { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Mascota> Mascotas { get; set; }
    }
}
