using Newtonsoft.Json;

namespace ProductShop.Data.DTO
{
    public class UserExportDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("soldProducts")]
        public SoldProductsExportDTO SoldProducts { get; set; }
    }
}
