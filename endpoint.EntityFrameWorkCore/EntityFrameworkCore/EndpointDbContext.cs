namespace endpoint.EntityFrameworkCore.EntityFrameworkCore
{
    using endpoint.Core.Products;
    using Microsoft.EntityFrameworkCore;

    public class EndpointDbContext : DbContext
    {
        public EndpointDbContext(DbContextOptions<EndpointDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
