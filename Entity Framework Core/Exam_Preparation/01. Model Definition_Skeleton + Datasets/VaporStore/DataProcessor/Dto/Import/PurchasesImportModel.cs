using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class PurchasesImportModel
    {
        [XmlAttribute("title")]
        [Required]
        public string Title { get; set; }

        [XmlElement("Type")]
        [EnumDataType(typeof(PurchaseType))]
        [Required]
        public string Type { get; set; }

        [XmlElement("Key")]
        [RegularExpression(@"^([A-Z0-9]{4})-([A-Z0-9]{4})-([A-Z0-9]{4})$")]
        [Required]
        public string Key { get; set; }

        [XmlElement("Card")]
        [Required]
        public string Card { get; set; }

        [XmlElement("Date")] 
        [Required]
        public string Date { get; set; }
    }
}
