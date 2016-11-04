using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class AttributeOptionDto : BaseDto
  {
    [XmlAttribute]
    public bool Default { get; set; }

    [XmlAttribute]
    public int Value { get; set; }

    public int Index { get; set; }

    [XmlElement("Translations")]
    public List<TranslationDto> Translations { get; set; }
  }
}
