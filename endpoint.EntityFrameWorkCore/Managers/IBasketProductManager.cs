namespace endpoint.EntityFrameworkCore.Managers
{
    using System.Threading.Tasks;
    using endpoint.Core.Baskets;
    using endpoint.EntityFrameworkCore.Repositories;

    public interface IBasketProductManager : IRepository<BasketProduct>
    {
        Task<BasketProduct> AddProductToBasket(int productId);
    }
}