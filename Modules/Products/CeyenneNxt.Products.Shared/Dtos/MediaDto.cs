using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class MediaDto : BaseNamedDto
  {
    public CultureDto Culture { get; set; }

    [XmlElement("Descriptions")]
    public List<DescriptionDto> Descriptions { get; set; }

    [XmlAttribute]
    public string Location { get; set; }
    [XmlElement]
    public MediaTypeDto MediaType { get; set; }
  }
}
