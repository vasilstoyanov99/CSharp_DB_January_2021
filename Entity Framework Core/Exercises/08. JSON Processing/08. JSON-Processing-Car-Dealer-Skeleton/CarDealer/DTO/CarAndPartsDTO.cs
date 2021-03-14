using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarDealer.DTO
{
    public class CarAndPartsDTO
    {
        public CarAndPartsDTO()
        {
            Parts = new List<PartDTO>();
        }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        [JsonProperty("parts")]
        public IEnumerable<PartDTO> Parts { get; set; }
    }
}
