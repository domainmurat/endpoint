using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using endpoint.Application.Shared.Baskets;
using endpoint.EntityFrameworkCore.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace endpoint.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketProductController : ControllerBase
    {
        private readonly IBasketProductManager _basketProductManager;

        public BasketProductController(IBasketProductManager basketProductManager)
        {
            _basketProductManager = basketProductManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketProductDto>>> Get()
        {
            return await _basketProductManager.GetAll().Select(bp => BasketProductMapper.Map(bp)).ToListAsync();
        }

        // POST: api/BasketProducts
        [HttpPost]
        public async Task<ActionResult<BasketProductDto>> AddProductToBasket([FromBody]int productId)
        {
            try
            {
                var basketProduct = await _basketProductManager.AddProductToBasket(productId);

                return CreatedAtAction("AddProductToBasket", new { id = basketProduct?.BasketId }, BasketProductMapper.Map(basketProduct));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }
    }
}
