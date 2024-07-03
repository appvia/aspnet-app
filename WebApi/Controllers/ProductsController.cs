using WebApi.MainDbContext;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MainDemoDbContext _mainDbContext;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ILogger<ProductsController> logger, MainDemoDbContext mainDbContext)
        {
            _logger = logger;
            _mainDbContext = mainDbContext;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await _mainDbContext.Products.ToListAsync();
        }

        
        [HttpPost]
        public async Task<Product> Post(ProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Weight = productDto.Weight,
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _mainDbContext.Add(product);
            await _mainDbContext.SaveChangesAsync();
            
            return product;
        }

        [HttpPut("{productId}")]
        public async Task<Product> Put(int productId, ProductDto productDto)
        {
            var product = await _mainDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Couldn't find the product");
            }
            product.Name = productDto.Name;
            product.Weight = productDto.Weight;
            product.UpdateDate = DateTime.Now;
            _mainDbContext.Update(product);
         
            await _mainDbContext.SaveChangesAsync();
            
            return product;
        }

        [HttpDelete("{productId}")]
        public async Task<Product> Delete(int productId)
        {
            var product = await _mainDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Couldn't find the product");
            }
            _mainDbContext.Remove(product);
            await _mainDbContext.SaveChangesAsync();
            return product;
        }
    }
}
