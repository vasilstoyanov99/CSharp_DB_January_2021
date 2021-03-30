using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class DepartmentsCellsImportModel
    {
        [StringLength(25, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public List<CellImportModel> Cells { get; set; }
    }
}
