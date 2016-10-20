using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CeyenneNxt.Orders.Shared.Dtos;

namespace GenerateOrders
{
  class Program
  {

    static void Main(string[] args)
    {
      ConcentratorSport2000Entities entities = new ConcentratorSport2000Entities();

      var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
      if (!Directory.Exists(filepath))
      {
        Directory.CreateDirectory(filepath);
      }
      

      var orders = entities.Order.Take(10);

      foreach (var order in orders)
      {
        var orderDto = MapOrderToOrderDto(entities,order);

        var filename = Path.Combine(filepath, String.Format("Order {0}.xml", orderDto.BackendID));


        Save(orderDto, filename);
      }
    }

    public static void Save<T>(T obj, String path)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(T));

      using (StreamWriter writer = new StreamWriter(path))
      {
        serializer.Serialize(writer, obj);
      }
    }

    private static OrderDto MapOrderToOrderDto(ConcentratorSport2000Entities entities, Order source)
    {
      OrderDto order = new OrderDto();
      order.BackendID = source.CustomerOrderReference;
      order.ID = source.OrderID;

      if (source.Customer1 != null)
      {
        var address = source.Customer1.Address;
        if (address != null)
        {
          order.Addresses.Add(new AddressDto()
          {
            Type = new AddressTypeDto() { Name = "Shipping" },
            BackendID = address.AddressID.ToString(),
            Street = address.AddressLine1,
            HouseNumber = address.HouseNumber,
            City = address.City,
            Country = new CountryDto() { Name = address.Country } 
            
          });
        }

      }

      if (source.Customer2 != null)
      {
       var address = source.Customer2.Address;
        if (address != null)
        {
          order.Addresses.Add(new AddressDto()
          {
            Type = new AddressTypeDto() { Name = "Billing" },
            BackendID = address.AddressID.ToString(),
            Street = address.AddressLine1,
            HouseNumber = address.HouseNumber,
            City = address.City,
            Country = new CountryDto() { Name = address.Country }

          });
        }

      }

      

      return order;
    }
  }
}
