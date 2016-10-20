using System;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.ServiceBus;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Interfaces;

namespace CeyenneNxt.Orders.Module.Processors
{
  public class OrderSubscribeProcessor : IOrderSubscribeProcessor
  {
    public ISettingModule SettingModule { get; private set; }

    public ILoggingModule LoggingModule { get; private set; }

    public IOrderModule OrderModule { get; private set; }

    public IServiceBusModule ServiceBusModule { get; private set; }

    public OrderSubscribeProcessor(ISettingModule settingModule, ILoggingModule loggingModule, IServiceBusModule serviceBusModule, IOrderModule orderModule)
    {
      SettingModule = settingModule;
      LoggingModule = loggingModule;
      ServiceBusModule = serviceBusModule;
      OrderModule = orderModule;
    }

    public virtual void Execute()
    {
      ServiceBusModule.InitReceiveQueue(QueueNames.OrderImportQueue);

      while (true)
      {
        ServiceBusModule.ReceiveFromQueue<OrderDto>(HandleOrder);
      }
    }

    protected virtual void HandleOrder(IMessageEnvelope<OrderDto> envelope)
    {
      var order = envelope.Item;

      OrderModule.CreateOrder(order);
    }

  }

  
}
