using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectWithTheirTasksExportModel
    {
        [XmlAttribute("TasksCount")]
        public string TasksCount { get; set; }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")] 
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskXmlExportModel[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class TaskXmlExportModel
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Label")]
        public string Label { get; set; }
    }
}
