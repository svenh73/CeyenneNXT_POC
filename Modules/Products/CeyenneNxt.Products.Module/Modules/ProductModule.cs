using System.Collections.Generic;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Products.Shared.Dtos;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Products.Module.Modules
{
    public class ProductModule: BaseModule, IProductModule
  {
      public IProductRepository ProductRepository { get; private set; } 
      public ISettingModule SettingModule { get; private set; }
      public ILoggingModule LoggingModule { get; private set; } 

      public ProductModule(IProductRepository productRepository, ISettingModule settingModule, ILoggingModule loggingModule)
      {
        ProductRepository = productRepository;
        SettingModule = settingModule;
        LoggingModule = loggingModule;
    }

      public virtual List<ProductDto> GetProducts()
      {
        var products = ProductRepository.GetProducts();
        var productDtos = new List<ProductDto>();
        products.ForEach(p => productDtos.Add(new ProductDto()
        {
          ID = p.ID,
          Name = p.Name
        }));
        return productDtos;
      }
    }
}
