namespace endpoint.EntityFrameworkCore.EntityFrameworkCore
{
    using endpoint.Core.Baskets;
    using endpoint.Core.Products;
    using Microsoft.EntityFrameworkCore;

    public class EndpointDbContext : DbContext
    {
        public EndpointDbContext(DbContextOptions<EndpointDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketProduct>().HasKey(bp => new { bp.BasketId, bp.ProductId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
