using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium;
using Product_Scraping.Models.Database;
using Product_Scraping.Models;

namespace Product_Scraping.Services
{
    public class ScrapingService
    {
        private IWebDriver _webDriver;
        public ScrapingService()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public async Task<List<Product>> FreeFoodFactsScraping()
        {
            var openFoodFactsUrl = "https://world.openfoodfacts.org/";
            _webDriver.Navigate().GoToUrl(openFoodFactsUrl);

            var items = _webDriver.FindElements(By.ClassName("list_product_a"));
            var itemsLimitados = items.ToList().Take(3);

            var urlsDeProduto = new List<string>();
            var produtos = new List<Product>();

            foreach (var item in itemsLimitados)
            {
                urlsDeProduto.Add(item.GetAttribute("href"));
            }

            foreach (var url in urlsDeProduto)
            {
                var produto = new Product();
                _webDriver.Navigate().GoToUrl(url);

                var elCode = _webDriver.FindElements(By.Id("barcode")).FirstOrDefault()?.Text;
                var elBarcode = _webDriver.FindElements(By.Id("barcode_paragraph")).FirstOrDefault()?.Text;
                var elNomeDoProduto = _webDriver.FindElements(By.ClassName("title-1")).FirstOrDefault()?.Text;
                var elQuantidade = _webDriver.FindElements(By.Id("field_quantity_value")).FirstOrDefault()?.Text;
                var elCategorias = _webDriver.FindElements(By.Id("field_categories_value")).FirstOrDefault()?.Text;
                var elEmbalagem = _webDriver.FindElements(By.Id("field_packaging_value")).FirstOrDefault()?.Text;
                var elMarcas = _webDriver.FindElements(By.Id("field_brands_value")).FirstOrDefault()?.Text;
                var elUrlDaImagem = _webDriver.FindElements(By.XPath("//img[@id='og_image']")).FirstOrDefault();

                var UrlDaImagem = elUrlDaImagem != null ? elUrlDaImagem.GetAttribute("src") : "";

                produto.Code = Convert.ToInt64(elCode);
                produto.BarCode = elBarcode.Replace("Barcode: ", "");
                produto.ProductName = elNomeDoProduto;
                produto.Status = Status.Draft;
                produto.ImportedT = DateTime.Now;
                produto.Url = url;
                produto.Quantity = elQuantidade;
                produto.Categories = elCategorias;
                produto.Packaging = elEmbalagem;
                produto.Brands = elMarcas;
                produto.ImageUrl = UrlDaImagem;

                produtos.Add(produto);

            }

            _webDriver.Quit();

            return produtos;
        }
    }
}
