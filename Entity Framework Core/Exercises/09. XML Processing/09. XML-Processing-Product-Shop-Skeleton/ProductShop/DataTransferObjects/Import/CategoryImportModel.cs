using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Import
{
    [XmlType("Category")]
    public class CategoryImportModel
    {
        [XmlElement("name")] 
        public string Name { get; set; }
    }
}
