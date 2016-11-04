using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CeyenneNxt.Core.Dtos;
using System.Xml.Serialization;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public abstract class BaseProductDto : BaseDto
  {
    [XmlElement("AttributeValues")]
    public List<ProductAttributeValueDto> AttributeValues { get; set; }

    [XmlElement("RelatedProducts")]
    public List<RelatedProductDto> RelatedProducts { get; set; }

    [XmlElement("Media")]
    public List<MediaDto> Media { get; set; }

    [XmlElement("Barcodes")]
    public List<ProductBarcodeDto> Barcodes { get; set; }
  }
}
