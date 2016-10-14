using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Interfaces;

namespace CeyenneNxt.Products.Shared.Interfaces
{
  public interface IProductSubscriberProcessor : IProcessor
  {
    void Execute(IProductModule productModule);
  }
}
