using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProductShop.Data.DTO
{
    public class SoldProductsDto
    {
        public SoldProductsDto()
        {
            SoldProducts = new List<ProductDto>();
        }

        [JsonProperty("firstName")] 
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")] 
        public ICollection<ProductDto> SoldProducts { get; set; }
    }
}
