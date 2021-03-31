using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficersPrisonersImportModel
    {
        [StringLength(30, MinimumLength = 3)]
        [Required]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Required]
        [XmlElement("Money")]
        public decimal Money { get; set; }

        [EnumDataType(typeof(Position))]
        [Required]
        [XmlElement("Position")]
        public string Position { get; set; }

        [EnumDataType(typeof(Weapon))]
        [Required]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        [Required]
        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        public PrisonerIdImportModel[] Prisoners { get; set; }
    }

    [XmlType("Prisoner")]
    public class PrisonerIdImportModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; } //TODO: Check if ID should be string
    }
}
