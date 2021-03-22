using System.Collections.Generic;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.ExportModels
{
    [XmlType("sale")]
    public class SalesWithAppliedDiscountEM
    {
        [XmlAttribute("sale")]
        public List<SaleWithDiscountEM> Sales { get; set; }
    }
}
