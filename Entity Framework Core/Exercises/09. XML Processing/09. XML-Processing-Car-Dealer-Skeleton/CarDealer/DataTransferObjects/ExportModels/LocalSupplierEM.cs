using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.ExportModels
{
    [XmlType("supplier")]
    public class LocalSupplierEM
    {
        [XmlAttribute("id")] 
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}
