using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class ProductDto : BaseProductDto
  {
    public ProductDto Parent { get; set; }

    [XmlElement]
    public OrganizationDto Organization { get; set; }

    public BrandDto Brand { get; set; }

    [XmlElement]
    public ProductTypeDto ProductType { get; set; }

    [XmlElement("VendorProducts")]
    public List<VendorProductDto> VendorProducts { get; set; }
  }
}
