using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.ServiceBus;

namespace CeyenneNxt.Products.Shared.Interfaces
{
  public interface IProductPublishProcessor : IProcessor
  {
    void Execute();
  }
}
