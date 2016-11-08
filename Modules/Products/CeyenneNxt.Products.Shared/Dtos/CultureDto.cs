using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class CultureDto : BaseReferenceDto
  {
    public bool Default { get; set; } = true;

    public OrganizationDto Organization { get; set; }

  }
}
