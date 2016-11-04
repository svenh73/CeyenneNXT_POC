using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.ServiceBus;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Settings.CoreModule;

namespace CeyenneNxt.Orders.Module.Processors
{
  public class OrderPublishProcessor : BaseProcessor, IOrderPublishProcessor
  {
    public ISettingModule SettingModule { get; private set; }

    public ILoggingModule LoggingModule { get; private set; }

    public IServiceBusModule ServiceBusModule { get; private set; }

    public IFileManagingModule FileManagingModule { get; private set; }

    public SettingCollection SettingCollection { get; private set; }

    public OrderPublishProcessor(ISettingModule settingModule, ILoggingModule loggingModule, IServiceBusModule serviceBusModule, IFileManagingModule fileManagingModule)
    {
      SettingModule = settingModule;
      LoggingModule = loggingModule;
      ServiceBusModule = serviceBusModule;
      FileManagingModule = fileManagingModule;
    }

    private void Init()
    {
      using (var session = new SettingModuleSession())
      {
        var collection = new SettingCollection(Domain, CNXTEnvironments.Current.EnvironmentType)
           .Add(new GeneralSettingDto() { Name = Constants.SettingNames.SourceDirectory, DataType = SettingDataType.String, Required = true })
           .Add(new GeneralSettingDto() { Name = Constants.SettingNames.SearchPattern, DataType = SettingDataType.Int, Required = true, DefaultValue = @"Order*.xml" });

        SettingCollection = SettingModule.LoadSettings(session, collection);
      }
    }

    public virtual void Execute()
    {
      try
      {
        Init();

        LoggingModule.LogInfo(GetType().ToString(), "Start publishing orders");

        var files = LoadFiles();

        foreach (var file in files)
        {
          LoggingModule.LogInfo(GetType().ToString(), $"Start processing file {file.FileName}");

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
            LoggingModule.LogError(GetType().ToString(), $"Error processing file {file.FileName} with message {ex.Message}", ex.StackTrace);

            file.MoveToErrorFolder(ex.Message);
          }
        }

        LoggingModule.LogInfo(GetType().ToString(), "End publishing orders");
      }
      catch (Exception)
      {
        
        throw;
      }
    }

    public virtual List<IFileHandler> LoadFiles()
    {
      FileManagingModule.SourceDirectory = SettingCollection[Constants.SettingNames.SourceDirectory].ToString(); //ConfigurationManager.AppSettings["SourcePath"];
      var searchpattern = SettingCollection[Constants.SettingNames.SearchPattern].ToString();

      var files = FileManagingModule.GetFiles(null, searchpattern);

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
