using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Dtos.RequestReponse
{
  public class RequestDto<T> where T : BaseDto
  {
    public T Data { get; set; }
  }
}
