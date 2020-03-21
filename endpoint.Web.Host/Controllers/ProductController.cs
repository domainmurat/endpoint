namespace endpoint.Web.Host.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using endpoint.Application.Shared.Products.Dto;
    using endpoint.Core.Products;
    using endpoint.EntityFrameworkCore.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await productRepository.GetAll().ToListAsync();
            return products.Select(p => Map(p)).ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await productRepository.Get(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Map(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var product = await productRepository.Get(p => p.Id == id);
            product.Name = productDto.Name;
            product.Stock = productDto.Stock;
            await productRepository.Update(product);

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            var product = ReverseMap(productDto);
            await productRepository.Add(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, Map(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.Get(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            await productRepository.Delete(product);

            return NoContent();
        }

        //I could use AutoMapper
        ProductDto Map(Product product)
        {
            return new ProductDto { Id = product.Id, Name = product.Name, Stock = product.Stock };
        }

        Product ReverseMap(ProductDto product)
        {
            return new Product { Id = product.Id, Name = product.Name, Stock = product.Stock };
        }
    }
}
