using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProductShop.Data.DTO
{
    public class SoldProductsExportDTO
    {
        public SoldProductsExportDTO()
        {
            Products = new List<ProductDTO>();
        }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("products")]
        public List<ProductDTO> Products { get; set; }
    }
}
