﻿using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Export
{
    [XmlType("SoldProducts")]
    public class SoldProductExportModel
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")] 
        public ProductExportModel[] Products { get; set; }
    }
}
