using Microsoft.AspNetCore.Mvc;
using Product_Scraping.Models;
using Product_Scraping.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_Scraping.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService) => _productsService = productsService;

        // GET: /<Products>
        [HttpGet]
        public async Task<List<Product>>  Get()
        {
            return await _productsService.GetAllAsync();
        }

        // GET /<Products>/5
        [HttpGet("{code}")]
        public async Task<ActionResult<Product>> Get(long code)
        {
            var product = await _productsService.GetAsync(code);
            if (product is null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
