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
    public DbSet<zefffood.Models.Pago> DataPago {get; set; }
    public DbSet<zefffood.Models.Pedido> DataPedido {get; set; }
    public DbSet<zefffood.Models.DetallePedido> DataDetallePedido {get; set; }

}
