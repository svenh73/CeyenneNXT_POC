﻿using System;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.ServiceBus;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Settings.CoreModule;

namespace CeyenneNxt.Orders.Module.Processors
{
  public class OrderSubscribeProcessor : BaseProcessor, IOrderSubscribeProcessor
  {
    public ISettingModule SettingModule { get; private set; }

    public ILoggingModule LoggingModule { get; private set; }

    public IOrderModule OrderModule { get; private set; }

    public IServiceBusModule ServiceBusModule { get; private set; }

    public SettingCollection SettingCollection { get; private set; }  

    public OrderSubscribeProcessor(ISettingModule settingModule, ILoggingModule loggingModule, IServiceBusModule serviceBusModule, IOrderModule orderModule)
    {
      SettingModule = settingModule;
      LoggingModule = loggingModule;
      ServiceBusModule = serviceBusModule;
      OrderModule = orderModule;
    }

    private void Init()
    {
      using (var session = new SettingModuleSession())
      {
        var collection = new SettingCollection(Domain, CNXTEnvironments.Current.EnvironmentType)
          .Add(new GeneralSettingDto()
          {
            Name = Constants.SettingNames.MaximumWaitSecondsForReceiveFromBus,
            DataType = SettingDataType.Int,
            Required = true,
            DefaultValue = "60"
          });

        SettingCollection = SettingModule.LoadSettings(session, collection);
      }
    }

    public virtual void Execute()
    {
      try
      {
        LoggingModule.LogInfo(Domain, "Start ordersubscribeprocessor");

        using (var session = new OrderModuleSession())
        {
          var maximumwaitseconds = SettingCollection[Constants.SettingNames.MaximumWaitSecondsForReceiveFromBus].ToInt();

          ServiceBusModule.InitReceiveQueue(QueueNames.OrderImportQueue);

          while (true)
          {
            LoggingModule.LogInfo(Domain, "Start receiving orders");
            ServiceBusModule.ReceiveFromQueue<OrderDto>(envelope => HandleOrder(session, envelope));
          }
        }

        LoggingModule.LogInfo(Domain, "Ended ordersubscribeprocessor");
      }
      catch (Exception ex)
      {
        LoggingModule.LogError(Domain,ex.Message,ex.StackTrace);
        throw;
      }
    }

    protected virtual void HandleOrder(IOrderModuleSession session, IMessageEnvelope<OrderDto> envelope)
    {
      try
      {
        var order = envelope.Item;

        OrderModule.CreateOrder(session, order);
        envelope.Complete();

        LoggingModule.LogInfo(Domain, $"Handled order {order.ID}");
      }
      catch (Exception ex)
      {
        // Todo: determine if the envelop has to be move to the deadletterqueue or that its has to be abrandon.
        envelope.Deadletter("Error in handling order",ex.Message);

        LoggingModule.LogError(Domain, ex.Message,ex.StackTrace);
        throw;
      }

     
    }

  }

  
}
