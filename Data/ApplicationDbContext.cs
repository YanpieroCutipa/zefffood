using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace zefffood.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){     
        }
    public DbSet<zefffood.Models.Contacto> DataContacto {get; set; }
    public DbSet<zefffood.Models.Producto> DataProducto {get; set; }
    public DbSet<zefffood.Models.Proforma> DataItemCarrito {get; set; }
}
