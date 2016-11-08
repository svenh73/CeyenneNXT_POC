using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class AttributeDto : BaseReferenceDto
  {
    public int Index { get; set; }

    [XmlAttribute]
    public AttributeType AttributeType { get; set; }
    [XmlAttribute]
    public AttributeDataType DataType { get; set; }

    [XmlElement]
    public OrganizationDto Organization { get; set; }

    public AttributeGroupDto AttributeGroup { get; set; }

    [XmlElement("Options")]
    public List<AttributeOptionDto> Options { get; set; }

    [XmlElement("Translations")]
    public List<TranslationDto> Translations { get; set; }

    public string ValidationExpression { get; set; }

  }
}
