using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.ServiceBus;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Products.Shared.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace CeyenneNxt.Orders.Module.Processors
{
  public class OrderPublishProcessor : IOrderPublishProcessor
  {
    public ISettingModule SettingModule { get; private set; }

    public ILoggingModule LoggingModule { get; private set; }

    public IProductModule ProductModule { get; private set; }

    public IServiceBusModule ServiceBusModule { get; private set; }

    public IFileManagingModule FileManagingModule { get; private set; }

    public OrderPublishProcessor(ISettingModule settingModule, ILoggingModule loggingModule, IServiceBusModule serviceBusModule, IFileManagingModule fileManagingModule)
    {
      SettingModule = settingModule;
      LoggingModule = loggingModule;
      ServiceBusModule = serviceBusModule;
      FileManagingModule = fileManagingModule;
    }

    public virtual void Execute()
    {
      var files = LoadFiles();

      foreach (var file in files)
      {
        try
        {
          var orderDto = LoadAndMapToOrderDto(file.FilePath);

          if (orderDto != null && ValidateOrder(orderDto))
          {
            var message = ServiceBusModule.CreateEnvelope<OrderDto>();
            message.Item = orderDto;
            message.MessageTypeAction = MessageTypeAction.Insert;

            ServiceBusModule.PublishToQueue(message, QueueNames.OrderImportQueue);
            file.MoveToSuccessFolder();

          }
        }
        catch (Exception ex)
        {
          file.MoveToErrorFolder(ex.Message);
        }
      }
    }

    public virtual List<IFileHandler> LoadFiles()
    {
      FileManagingModule.SourceDirectory = ConfigurationManager.AppSettings["SourcePath"];
      var files = FileManagingModule.GetFiles(null, @"Order*.xml");

      return files;
    }

    public virtual bool ValidateOrder(OrderDto order)
    {
      if (String.IsNullOrEmpty((order.BackendID)))
      {
        throw new NotSupportedException("Required 'BackendID' not available for this order");
      }
      return true;
    }

    public virtual OrderDto LoadAndMapToOrderDto(string filepath)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(OrderDto));
      using (FileStream fs = new FileStream(filepath, FileMode.Open))
      {
        return (OrderDto) serializer.Deserialize(fs);
      }
    }

  }

}
