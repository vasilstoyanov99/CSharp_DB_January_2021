using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Export
{
    [XmlType("Users")]
    public class UsersCountExportModel
    {
        [XmlElement("count")] 
        public int Count { get; set; }

        [XmlArray("users")] 
        public UsersAndProductsExportModel[] Users { get; set; }
    }
}
