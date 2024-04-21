using System.Xml.Serialization;

namespace DiscountGenerator.ExportModels
{
    [XmlRoot(ElementName = "ProductInfo")]
    public class XMLProductInfoExportModel
    {
        [XmlElement(ElementName = "Discount")]
        public DiscountExport Discount { get; set; }
    }
    [XmlRoot(ElementName = "Discount")]
    public class DiscountExport
    {
        private string percent;
        [XmlElement(ElementName = "Percent")]
        public string Percent
        {
            get { return percent; }
            set
            {
                percent = value == "0" ? null : value;
            }
        }

        private string fix;
        [XmlElement(ElementName = "Fix")]
        
        public  string Fix
        {
            get { return fix; }
            set
            {
              fix = value == "0" ? null : value; 
            }
        }
    }

}
