using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class RelatedProductDto : BaseDto
  {
    [XmlElement]
    public ProductDto RelatedProduct { get; set; }

    [XmlElement]
    public RelatedProductTypeDto RelatedProductType { get; set; }
  }
}
