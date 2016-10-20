using System.Collections.Generic;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Products.Shared.Entitites;

namespace CeyenneNxt.Products.Shared.Interfaces
{
  public interface IProductRepository
  {
    List<Product> GetProducts();

  }
}
