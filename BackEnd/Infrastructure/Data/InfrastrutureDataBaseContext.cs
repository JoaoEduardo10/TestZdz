using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class InfrastrutureDataBaseContext : DbContext
    {
        public InfrastrutureDataBaseContext(DbContextOptions<InfrastrutureDataBaseContext> options) : base(options)
        {
        }

        public DbSet<PedidosEntity> PedidosEntity { get; set; }
        public DbSet<ProdutosEntity> ProdutosEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
