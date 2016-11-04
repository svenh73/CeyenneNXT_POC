using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Dtos.RequestReponse
{
  public class ValidationErrorDto
  {
    public string XPath { get; set; } // Use with ObjectXPathNavigator
    public string Message { get; set; }
  }
}
