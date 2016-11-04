using System.Collections.Generic;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Products.Shared;
using CeyenneNxt.Products.Shared.Entitites;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Products.Module.Repositories
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository()
    {
      
    }

    public List<Product> GetProducts(IProductModuleSession session)
    {
      var products = new List<Product>();
      products.Add(new Product () {ID = 1,Name = "Product A"});
      products.Add(new Product() { ID = 2, Name = "Product B" });
      return products;
    }
  }
}
