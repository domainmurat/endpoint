namespace endpoint.Application.Shared.Baskets
{
    using endpoint.Core.Baskets;

    public static class BasketMapper
    {
        public static BasketDto Map(Basket basket)
        {
            return new BasketDto { Id = basket.Id, Name = basket.Name };
        }
    }
}
