using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Products.Shared.Dtos;

namespace CeyenneNxt.Products.Shared.Interfaces
{
  public interface IProductCommunicator : ICommunicator
  {
    Task<string> PostProduct(ProductDto product);

  }
}
