using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Core.Dtos;

namespace CeyenneNxt.Products.Shared.Dtos
{
  public class ProductPriceDto : BaseDto
  {
    [XmlAttribute]
    public decimal Price { get; set; }

    [XmlAttribute]
    public bool IncludingVAT { get; set; }

    public DateTime Startdate { get; set; }

    [XmlElement]
    public PriceTypeDto PriceType { get; set; }
    [XmlElement]
    public CurrencyTypeDto Currency { get; set; }
    [XmlElement]
    public VATTypeDto VAT { get; set; }

  }
}
