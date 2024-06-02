using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class InfrastrutureDataBaseContext : DbContext
    {
        public InfrastrutureDataBaseContext(DbContextOptions<InfrastrutureDataBaseContext> options) : base(options)
        {
        }

        public DbSet<OrderEntity> OrdersEntity { get; set; }
        public DbSet<ProductEntity> ProductsEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
