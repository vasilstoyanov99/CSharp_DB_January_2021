using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Import
{
    [XmlType("CategoryProduct")]
    public class CategoryProductImportModel
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        [XmlElement("ProductId")] 
        public int ProductId { get; set; }
    }
}
