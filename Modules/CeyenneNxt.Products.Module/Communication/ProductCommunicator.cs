using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Products.Shared.Dtos;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Products.Module.Communication
{
  public class ProductCommunicator : IProductCommunicator
  {
    public Task<string> PostProduct(ProductDto product)
    {
      throw new NotImplementedException();
    }
  }
}
