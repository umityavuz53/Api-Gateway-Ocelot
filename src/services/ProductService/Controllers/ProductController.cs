namespace ProductService.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("suggestions/{id}")]
        public IEnumerable<Product> Get(string id)
        {
            var products = new List<Product>
                               {
                                   new Product { Id = 1, Title = "Commandos III", Price = 34.50 },
                                   new Product { Id = 2, Title = "Table Child", Price = 23.67 },
                                   new Product { Id = 3, Title = "League of Heros 2022", Price = 145.99 }
                               };

            return products;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }
}
