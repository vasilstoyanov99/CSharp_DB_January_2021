using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using BookShop.Data.Models.Enums;

namespace BookShop.DataProcessor.ImportDto
{
    [XmlType("Book")]
    public class BookImportModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("Genre")]
        [EnumDataType(typeof(Genre))]
        public string Genre { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [XmlElement("Price")]
        public decimal Price { get; set; }

        [Required]
        [Range(50, 5000)]
        [XmlElement("Pages")]
        public int Pages { get; set; }

        [Required]
        [XmlElement("PublishedOn")]
        public string PublishedOn { get; set; }
    }
}
