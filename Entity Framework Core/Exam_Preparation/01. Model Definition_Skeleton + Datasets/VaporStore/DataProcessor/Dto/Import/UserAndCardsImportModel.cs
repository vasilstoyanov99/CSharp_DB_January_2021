using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class UserAndCardsImportModel
    {
        [Required]
        [RegularExpression(@"^([A-Z]{1}[a-z]+) ([A-Z]{1}[a-z]+)$")]
        public string FullName { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(3, 103)]
        public int Age { get; set; }

        public List<CardImportModel> Cards { get; set; }
    }

    public class CardImportModel
    {
        [Required]
        [RegularExpression(@"^([0-9]{4}) ([0-9]{4}) ([0-9]{4}) ([0-9]{4})$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{3})$")]
        public string CVC { get; set; }

        [Required]
        [EnumDataType(typeof(CardType))]
        public string Type { get; set; }
    }
}
