using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Export
{
    [XmlType("User")]
    public class UsersAndProductsExportModel
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        public SoldProductExportModel SoldProducts { get; set; }
    }
}
