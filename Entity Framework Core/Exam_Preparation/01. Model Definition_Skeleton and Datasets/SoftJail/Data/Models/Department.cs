using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.Data.Models
{
    public class Department
    {
        public Department()
        {
            Cells = new HashSet<Cell>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
