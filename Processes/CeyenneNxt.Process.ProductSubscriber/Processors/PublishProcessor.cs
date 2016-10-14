using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Products.Shared.Dtos;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Process.Product.Processors
{
  public class ProductPublishProcessor : IProductPublishProcessor
  {
    public ISettingModule SettingModule { get; set; }

    public ILoggingModule LoggingModule { get; set; }

    public IProductModule ProductModule { get; set; }

    public void Execute(IProductModule productModule, ISettingModule settingModule, ILoggingModule loggingModule)
    {
      throw new System.NotImplementedException();
    }

    public virtual ProductDto MapToProductDto(object sourceproduct)
    {
      return new ProductDto();
    }

    public virtual void LoadData()
    {
      
    }
  }
}
