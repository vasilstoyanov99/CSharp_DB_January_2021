using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProductShop.Data.DTO
{
    public class SoldProductsDTO
    {
        public SoldProductsDTO()
        {
            SoldProducts = new List<ProductDTO>();
        }

        [JsonProperty("firstName")] 
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")] 
        public ICollection<ProductDTO> SoldProducts { get; set; }
    }
}
