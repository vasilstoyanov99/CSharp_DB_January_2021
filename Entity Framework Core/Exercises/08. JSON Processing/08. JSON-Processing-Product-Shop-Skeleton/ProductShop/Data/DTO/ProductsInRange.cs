using Newtonsoft.Json;

namespace ProductShop.Data.DTO.ProductsDTO
{
    public class ProductsInRange
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("seller")]
        public string Seller { get; set; }
    }
}
