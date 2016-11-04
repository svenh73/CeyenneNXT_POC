using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class DescriptionDto
  {
    [XmlAttribute]
    public string Description { get; set; }

    [XmlElement]
    public CultureDto Culture { get; set; }
  }
}
