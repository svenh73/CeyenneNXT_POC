using System.Collections.Generic;
using CeyenneNxt.Products.Shared.Dtos;

namespace CeyenneNxt.Products.Shared.Interfaces
{
    public interface IProductModule
    {
      List<ProductDto> GetProducts(IProductModuleSession session);
    }
}
