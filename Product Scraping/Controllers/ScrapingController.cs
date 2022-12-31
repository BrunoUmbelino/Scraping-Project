using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Product_Scraping.Models;
using Product_Scraping.Services;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Product_Scraping.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ScrapingController : ControllerBase
    {
        private readonly ScrapingService _scrapingService;
        private readonly ProductsService _productsService;

        public ScrapingController(ProductsService productsService, ScrapingService scrapingService)
        {
            _productsService = productsService;
            _scrapingService = scrapingService;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            var produtos = await _scrapingService.FreeFoodFactsScraping();
            return await _productsService.CreateManyAsync(produtos);
        }
    }
}
