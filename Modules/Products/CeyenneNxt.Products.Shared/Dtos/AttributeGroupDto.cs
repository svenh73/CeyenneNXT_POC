using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class AttributeGroupDto : BaseReferenceDto
  {
    [XmlElement]
    public OrganizationDto Organization { get; set; }

    public AttributeGroupDto Parent { get; set; }
  }
}
