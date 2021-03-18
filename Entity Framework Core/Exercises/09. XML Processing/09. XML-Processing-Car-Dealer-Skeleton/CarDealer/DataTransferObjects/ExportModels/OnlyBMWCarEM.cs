using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.ExportModels
{
    [XmlType("car")]
    public class OnlyBMWCarEM
    {
        [XmlAttribute("id")] 
        public int Id { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
