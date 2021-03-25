using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Export
{
    [XmlType("Product")]
    public class ProductExportModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
