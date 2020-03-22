namespace endpoint.Application.Shared.Baskets
{
    using endpoint.Application.Shared.Products;
    using endpoint.Core.Baskets;

    public static class BasketProductMapper
    {
        public static BasketProductDto Map(BasketProduct basketProduct)
        {
            if (basketProduct == null) return null;

            return new BasketProductDto
            {
                BasketId = basketProduct.BasketId,
                Basket = BasketMapper.Map(basketProduct.Basket),
                ProductId = basketProduct.ProductId,
                Product = ProductMapper.Map(basketProduct.Product),
                Quantity = basketProduct.Quantity
            };
        }
    }
}
