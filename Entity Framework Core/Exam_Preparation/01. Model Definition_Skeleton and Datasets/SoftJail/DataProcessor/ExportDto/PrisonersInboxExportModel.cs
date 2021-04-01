﻿using System;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class PrisonersInboxExportModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public MessageExportModel[] EncryptedMessages { get; set; }
    }

    [XmlType("Message")]
    public class MessageExportModel
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
