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
  public class MediaTypeDto : BaseNamedCodeDto
  {
    [XmlElement("Translations")]
    public List<TranslationDto> Translations { get; set; }
  }
}
