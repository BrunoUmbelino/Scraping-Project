using Microsoft.OpenApi.Any;
using Product_Scraping.Models;
using Product_Scraping.Services;
using Quartz;
using System.Diagnostics;

namespace Product_Scraping.Jobs
{
    public class ScrapingJob : IJob
    {
        private readonly ScrapingService _scrapingService;
        private readonly ProductsService _productsService;

        public ScrapingJob(ScrapingService scrapingService, ProductsService productsService)
        {
            _scrapingService = scrapingService;
            _productsService = productsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Running Job.");
            var produtos = _scrapingService.FreeFoodFactsScraping();
            await _productsService.CreateManyAsync(produtos);
            await Task.CompletedTask;
        }
    }
}
