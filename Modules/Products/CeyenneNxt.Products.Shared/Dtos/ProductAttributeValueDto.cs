using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class ProductAttributeValueDto : BaseDto
  {
    [XmlElement]
    public AttributeDto Attribute { get; set; }

    public AttributeOptionDto AttributeOption { get; set; }

    public string Value { get; set; }

    public CultureDto Culture { get; set; }
  }
}
