using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Export
{
    [XmlType("User")]
    public class UsersSoldProductsExportProducts
    {
        [XmlElement("firstName")] 
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public SoldProductExportModel[] SoldProducts { get; set; }
    }
}
