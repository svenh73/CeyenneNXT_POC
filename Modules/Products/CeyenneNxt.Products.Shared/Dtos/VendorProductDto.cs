using System.Collections.Generic;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class VendorProductDto : BaseProductDto
  {
    [XmlElement]
    public VendorDto Vendor { get; set; }

    [XmlElement("Prices")]
    public List<ProductPriceDto> Prices { get; set;}

    [XmlElement("Stock")]
    public List<StockDto> Stock { get; set; }
  }
}
