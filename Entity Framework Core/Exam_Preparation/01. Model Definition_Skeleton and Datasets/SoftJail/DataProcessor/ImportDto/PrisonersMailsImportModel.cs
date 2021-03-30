using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class PrisonersMailsImportModel
    {
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string FullName { get; set; }

        [RegularExpression(@"^The [A-Z][a-z]+$")]
        [Required]
        public string Nickname { get; set; }

        [Range(18, 65)]
        [Required]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public List<MailImportModel> Mails { get; set; }

        public class MailImportModel
        {
            [Required]
            public string Description { get; set; }

            [Required]
            public string Sender { get; set; }

            [RegularExpression(@"^[A-Za-z0-9 ]*(str.)$")]
            [Required]
            public string Address { get; set; }
        }
    }
}
