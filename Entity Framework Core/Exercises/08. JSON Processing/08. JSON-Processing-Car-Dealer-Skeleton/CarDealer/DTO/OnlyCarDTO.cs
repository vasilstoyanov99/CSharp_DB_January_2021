using Newtonsoft.Json;

namespace CarDealer.DTO
{
    public class OnlyCarDTO
    {
        [JsonProperty("car")]
        public CarAndPartsDTO Car { get; set; }
    }
}
