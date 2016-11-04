using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Dtos.RequestReponse
{
  public class ResponseDto<T> where T : BaseDto
  {
    public T Data { get; set; }

    public List<ValidationErrorDto> ValidationErrors { get; set; }

    public bool Success { get; set; }

  }
}
