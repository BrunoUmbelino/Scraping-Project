﻿namespace Product_Scraping.Models.Database
{
    public class FreeFoodFactsScrapingDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProductsCollectionName { get ; set ; } = string.Empty;
    }
}
