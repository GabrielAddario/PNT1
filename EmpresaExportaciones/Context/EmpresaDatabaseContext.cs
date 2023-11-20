using EmpresaExportaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpresaExportaciones.Context
{
    public class EmpresaDatabaseContext : DbContext
    {
        public EmpresaDatabaseContext(DbContextOptions<EmpresaDatabaseContext> options): base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

    }
}
