using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium;
using Product_Scraping.Models.Database;
using Product_Scraping.Models;
using ZstdSharp.Unsafe;

namespace Product_Scraping.Services
{
    public class ScrapingService
    {
        private IWebDriver ?_webDriver;
        public ScrapingService() => new DriverManager().SetUpDriver(new ChromeConfig());

        public List<Product> FreeFoodFactsScraping()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            var openFoodFactsUrl = "https://world.openfoodfacts.org/";
            _webDriver.Navigate().GoToUrl(openFoodFactsUrl);

            var productElementsList = _webDriver.FindElements(By.ClassName("list_product_a"));
            var limitedListProductElements = productElementsList.ToList().Take(100);

            var ProductUrls = new List<string>();
            foreach (var productEl in limitedListProductElements)
            {
                ProductUrls.Add(productEl.GetAttribute("href"));
            }

            var products = new List<Product>();
            foreach (var url in ProductUrls)
            {
                _webDriver.Navigate().GoToUrl(url);

                var code = _webDriver.FindElements(By.Id("barcode")).FirstOrDefault()?.Text;
                var barcode = _webDriver.FindElements(By.Id("barcode_paragraph")).FirstOrDefault()?.Text;
                var productName = _webDriver.FindElements(By.ClassName("title-1")).FirstOrDefault()?.Text;
                var quantity = _webDriver.FindElements(By.Id("field_quantity_value")).FirstOrDefault()?.Text;
                var categories = _webDriver.FindElements(By.Id("field_categories_value")).FirstOrDefault()?.Text;
                var packaging = _webDriver.FindElements(By.Id("field_packaging_value")).FirstOrDefault()?.Text;
                var brands = _webDriver.FindElements(By.Id("field_brands_value")).FirstOrDefault()?.Text;
                var imageUrl = _webDriver.FindElements(By.XPath("//img[@id='og_image']")).FirstOrDefault();

                var product = new Product();
                if(code != null) product.Code = Convert.ToInt64(code);
                if(barcode != null) product.BarCode = barcode.Replace("Barcode: ", "");
                if (productName != null) product.ProductName = productName; 
                product.Url = url;
                if (quantity != null) product.Quantity = quantity;
                if (categories != null) product.Categories = categories;
                if (packaging != null) product.Packaging = packaging;
                if (brands != null) product.Brands = brands;
                if (imageUrl != null) product.ImageUrl = imageUrl.GetAttribute("src");

                products.Add(product);
            }

            _webDriver.Close();
            return products;
        }
    }
}
