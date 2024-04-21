using System.Xml.Serialization;

namespace DiscountGenerator.XMLModels.ImportModels
{
    [XmlRoot(ElementName = "ProductInfo")]
    public class XMLProductInfoImportModel
    {
        [XmlElement(ElementName = "Discount")]
        public DiscountImport DiscountImport { get; set; }
    }
    public class DiscountImport
    {
        [XmlElement(ElementName = "Percent")]
        public decimal Percent {  get; set; }

        [XmlElement(ElementName = "Fix")]
        public decimal Fix {  get; set; }

        public int ProductId { get; set; }
    }
}
