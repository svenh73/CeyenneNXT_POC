using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class StockDto : BaseDto
  {
    [XmlAttribute]
    public int Quantity { get; set; }

    [XmlAttribute]
    public StockTypeDto StockType { get; set; }
  }
}
