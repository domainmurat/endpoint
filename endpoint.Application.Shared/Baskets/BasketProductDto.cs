namespace endpoint.Application.Shared.Baskets
{
    using endpoint.Application.Shared.Products.Dto;

    public class BasketProductDto
    {
        public int BasketId { get; set; }
        public BasketDto Basket { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
    public class BasketDto : EntityDto
    {
        public string Name { get; set; }
    }
}
