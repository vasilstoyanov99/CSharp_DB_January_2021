using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.ExportModels
{
    [XmlType("car")]
    public class CarsEM
    {
        [XmlAttribute("make")] 
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")] 
        public PartsEM[] Parts { get; set; }
    }
}
