using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using endpoint.Core.Products;
using endpoint.EntityFrameworkCore.EntityFrameworkCore;
using endpoint.EntityFrameworkCore.Repositories;
using endpoint.Application.Shared.Products.Dto;

namespace endpoint.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> productRepository;

        public ProductsController(IRepository<Product> productRepository)
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
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await productRepository.Update(ReverseMap(product));

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto product)
        {
            await productRepository.Add(ReverseMap(product));

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
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
