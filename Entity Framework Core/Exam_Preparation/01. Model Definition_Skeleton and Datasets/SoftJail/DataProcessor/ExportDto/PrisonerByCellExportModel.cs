using System.Collections.Generic;

namespace SoftJail.DataProcessor.ExportDto
{
    public class PrisonerByCellExportModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CellNumber { get; set; }

        public List<OfficerExportModel> Officers { get; set; }

        public decimal TotalOfficerSalary { get; set; }
    }

    public class OfficerExportModel
    {
        public string OfficerName { get; set; }

        public string Department { get; set; }
    }
}
