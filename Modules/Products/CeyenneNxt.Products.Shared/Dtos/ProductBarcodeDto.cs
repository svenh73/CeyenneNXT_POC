using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class ProductBarcodeDto : BaseDto
  {
    public VendorDto Vendor { get; set; }
    [XmlAttribute]
    public string Barcode { get; set; }

    [XmlElement]
    public BarcodeTypeDto BarcodeType { get; set; }

    public bool IsDefault { get; set; }
  }
}
