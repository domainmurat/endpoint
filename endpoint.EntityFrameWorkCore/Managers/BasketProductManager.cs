namespace endpoint.EntityFrameworkCore.Managers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using endpoint.Core.Baskets;
    using endpoint.Core.Products;
    using endpoint.EntityFrameworkCore.EntityFrameworkCore;
    using endpoint.EntityFrameworkCore.Repositories;

    public class BasketProductManager : EFRepository<BasketProduct>, IBasketProductManager
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Basket> _basketRepository;

        public BasketProductManager(IRepository<Product> productRepository,
            IRepository<Basket> basketRepository,
            EndpointDbContext endpointDbContext) : base(endpointDbContext)
        {
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }

        public async Task<BasketProduct> AddProductToBasket(int productId)
        {
            var product = await _productRepository.Get(p => p.Id == productId);

            if (product == null) throw new ArgumentException($"There is no product with given {nameof(productId)}: {productId}.");

            if (product.Stock == 0) throw new ArgumentException($"{product.Name} has no stock.");

            var hasBasket = _basketRepository.GetAll().Any(p => p.UserId == 1);

            if (!hasBasket)
            {
                await _basketRepository.Add(new Basket { UserId = 1, Name = "User1 Basket" });
            }

            var loggedUSerBasket = await _basketRepository.Get(p => p.UserId == 1);

            var basketProduct = await Get(bp => bp.BasketId == loggedUSerBasket.Id && bp.ProductId == productId);
            if (basketProduct == null)
            {
                await Add(new BasketProduct { BasketId = loggedUSerBasket.Id, ProductId = productId, Quantity = 1 });
            }
            else
            {
                basketProduct.Quantity++;
                await Update(basketProduct);
            }

            product.Stock--;
            await _productRepository.Update(product);
            return await Get(bp => bp.BasketId == loggedUSerBasket.Id && bp.ProductId == productId);
        }
    }
}
