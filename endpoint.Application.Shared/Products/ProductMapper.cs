namespace endpoint.Application.Shared.Products
{
    using endpoint.Application.Shared.Products.Dto;
    using endpoint.Core.Products;
    public class ProductMapper
    {
        public static ProductDto Map(Product product)
        {
            return new ProductDto { Id = product.Id, Name = product.Name, Stock = product.Stock };
        }

        public static Product ReverseMap(ProductDto product)
        {
            return new Product { Id = product.Id, Name = product.Name, Stock = product.Stock };
        }
    }
}
