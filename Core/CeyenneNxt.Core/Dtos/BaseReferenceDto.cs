using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CeyenneNxt.Core.Dtos
{
  public class BaseReferenceDto : BaseNamedDto
  {
    [XmlAttribute]
    public string Code { get; set; }
  }
}
