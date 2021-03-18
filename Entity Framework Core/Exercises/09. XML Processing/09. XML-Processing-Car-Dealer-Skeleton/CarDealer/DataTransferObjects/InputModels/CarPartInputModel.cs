using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.InputModels
{
    [XmlType("partId")]
    public class CarPartInputModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
