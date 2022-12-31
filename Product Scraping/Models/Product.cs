using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product_Scraping.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonElement("code")]
        public long Code { get; set; }

        [BsonElement("barcode")]
        public string BarCode { get; set; }

        [BsonElement("status")]
        public Status Status { get; set; }
        
        [BsonElement("imported_t")]
        public DateTime ImportedT { get; set; }
        
        [BsonElement("url")]
        public string Url { get; set; }
        
        [BsonElement("product_name")]
        public string ProductName { get; set; }

        [BsonElement("quantity")]
        public string Quantity { get; set; }

        [BsonElement("categories")]
        public string Categories { get; set; }

        [BsonElement("packaging")]
        public string Packaging { get; set; }

        [BsonElement("brands")]
        public string Brands { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        public Product()
        {
            Code = 0;
            BarCode = "";
            Status = Status.Draft;
            ImportedT = new DateTime();
            Url = "";
            ProductName = "";
            Quantity = "";
            Categories = "";
            Packaging = "";
            Brands = "";
            ImageUrl = "";
        }
    }
}
