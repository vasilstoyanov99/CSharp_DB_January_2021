using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Export
{
    [XmlType("Product")]
    public class ProductsInRangeExportModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")] 
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public string BuyerName { get; set; }
    }
}
