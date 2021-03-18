using System.Collections.Generic;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.InputModels
{
    [XmlType("Car")]
    public class CarInputModel
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public List<CarPartInputModel> Parts { get; set; }
    }
}
