using System;
using System.Collections.Generic;
using System.Linq;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.ServiceBus;
using CeyenneNxt.ServiceBus.CoreModule.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace CeyenneNxt.ServiceBus.CoreModule
{
  public class ServiceBusModule : IServiceBusModule
  {
    private static string ConnectionString => CNXTEnvironments.Current.ServiceBusConnectionString;

    public ServiceBusModule()
    {
      AppContext.SetSwitch("Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate", true);
    }

    private NamespaceManager _namespaceManager;

    /// <summary>
    /// Manager which manages/creates this servicebus
    /// </summary>
    protected NamespaceManager NamespaceManager => _namespaceManager ?? (_namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString));


    private SubscriptionClient _subscriptionClient;

    private QueueClient _queueClient;

    /// <summary>
    /// Returns the client which is used to communicate with the servicebus
    /// </summary>
    public SubscriptionClient SubscriptionClient => _subscriptionClient;

    public void Dispose()
    {
      _namespaceManager = null;
      _subscriptionClient?.Close();
    }

    //Validate if subscription is valid
    // ReSharper disable once UnusedParameter.Local
    private void ValidateReceive(string subscriptionName)
    {
      if (string.IsNullOrEmpty(subscriptionName))
      {
        throw new Exception("Required SubscriptionName not configured");
      }
    }

    public IMessageEnvelope<TDto> CreateEnvelope<TDto>()
    {
      return new MessageEnvelope<TDto>();
    } 

    public void PublishToQueue<T>(IMessageEnvelope<T> item, string queueName, string vendorName = null, string channelName = null)
    {
      if (!NamespaceManager.QueueExists(queueName))
      {
        NamespaceManager.CreateQueue(new QueueDescription(queueName)
        {
          UserMetadata = "todo:add some readable topic information",
          EnableDeadLetteringOnMessageExpiration = true,
          DefaultMessageTimeToLive = new TimeSpan(1,0,0,0),
          AutoDeleteOnIdle = new TimeSpan(5, 0, 0, 0) //Todo: make configurable
        });
      }

      var message = new BrokeredMessage(item);

      //TODO: Add filter properties
      //ex: message.Properties.Add(Constants.BrokeredMessageProperties.VendorName, vendorName);


      var client = QueueClient.CreateFromConnectionString(ConnectionString, queueName);
      try
      {
        client.Send(message);
      }
      catch (Exception ex)
      {
        if (message.DeliveryCount > 3)
        {
          message.DeadLetter(ErrorMessages.MessageQueue_SendMessageException, ex.Message);
        }
        throw;
      }
    }

    /// <summary>
    ///   Publish the item on the configured topic
    /// </summary>
    /// <param name="item">The envelop with the item to publish</param>
    /// <param name="topicName"></param>
    /// <param name="vendorName"></param>
    /// <param name="channelName"></param>
    /// <returns>true if succesfull</returns>
    public void PublishToTopic<T>(IMessageEnvelope<T> item, string topicName, string vendorName = null, string channelName = null)
    {
      if (!NamespaceManager.TopicExists(topicName))
      {
        NamespaceManager.CreateTopic(new TopicDescription(topicName)
        {
          UserMetadata = "todo:add some readable topic information",
          AutoDeleteOnIdle = new TimeSpan(5, 0, 0, 0) //Todo: make configurable
        });
      }

      var message = new BrokeredMessage(item);

      //TODO: Add filter properties
      //ex: message.Properties.Add(Constants.BrokeredMessageProperties.VendorName, vendorName);


      var client = TopicClient.CreateFromConnectionString(ConnectionString, topicName);
      try
      {
        client.Send(message);
      }
      catch (Exception ex)
      {
        if (message.DeliveryCount > 3)
        {
          message.DeadLetter(ErrorMessages.MessageQueue_SendMessageException, ex.Message);
        }
        throw;
      }
    }

    /// <summary>
    /// Registers and/or initalizes the subscription which receives the messages put on the topic specified
    /// </summary>
    /// <param name="topicName">The topic to subscribe on</param>
    /// <param name="subscriptionName">Subscription name to receive messages from</param>
    /// <param name="sqlFilter">Optional filter to filer certain messages</param>
    public void InitReceiveTopic(string topicName, string subscriptionName, string sqlFilter = null)
    {

      ValidateReceive(subscriptionName);

      if (!NamespaceManager.TopicExists(topicName))
      {
        NamespaceManager.CreateTopic(topicName);
      }

      //verify if the subscription exists for this client
      if (!NamespaceManager.SubscriptionExists(topicName, subscriptionName))
      {
        try
        {
          //no subscription foud, so create a new one
          var description = new SubscriptionDescription(topicName, subscriptionName)
          {
            // Todo: Make this options configurable
            AutoDeleteOnIdle = new TimeSpan(5, 0, 0, 0),
            DefaultMessageTimeToLive = new TimeSpan(1, 0, 0, 0),
            LockDuration = new TimeSpan(0, 0, 5, 0),
            EnableBatchedOperations = true
          };


          if (string.IsNullOrEmpty(sqlFilter))
          {
            NamespaceManager.CreateSubscription(description);
          }
          else
          {
            var filter = new SqlFilter(sqlFilter);
            NamespaceManager.CreateSubscription(description, filter);
          }

        }
        catch (Exception ex)
        {
          //TODO: log it..
          throw new Exception($"The topic \"{topicName}\" could not be created!", ex);
        }
      }

      _subscriptionClient = SubscriptionClient.CreateFromConnectionString(ConnectionString, topicName, subscriptionName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queueName"></param>
    public void InitReceiveQueue(string queueName)
    {

      ValidateReceive(queueName);

      if (!NamespaceManager.QueueExists(queueName))
      {
        NamespaceManager.CreateQueue(queueName);
      }

      _queueClient = QueueClient.CreateFromConnectionString(ConnectionString, queueName, ReceiveMode.PeekLock);
    }

    /// <summary>
    /// Receive messages from the servicebus, this should be called after @InitReceive
    /// </summary>
    /// <typeparam name="T">Type of object returned from the bus</typeparam>
    /// <param name="compleetAction">Action will contain the message</param>
    /// <param name="maxWaitSeconds">Time to wait until messages are received</param>
    /// <returns></returns>
    public bool ReceiveFromTopic<T>(Action<IMessageEnvelope<T>> compleetAction, int maxWaitSeconds = 60)
    {
      if (_subscriptionClient == null)
      {
        throw new Exception("InitReceive not called, SubscriptionClient not initialized");
      }
      var brokkeredMessage = SubscriptionClient.Receive(TimeSpan.FromSeconds(maxWaitSeconds));
      if (brokkeredMessage != null)
      {
        var envelope = brokkeredMessage.GetBody<MessageEnvelope<T>>();

        try
        {
          compleetAction(envelope);
          brokkeredMessage.Complete();
        }
        catch (Exception)
        {
          brokkeredMessage.Abandon();

          //TODO: Add logging

        }
        return true;
      }
      else
      {
        return false;
      }
      
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="compleetAction"></param>
    /// <param name="maxWaitSeconds"></param>
    /// <returns></returns>
    public bool ReceiveFromQueue<T>(Action<IMessageEnvelope<T>> compleetAction, int maxWaitSeconds = 60)
    {
      if (_queueClient == null)
      {
        throw new Exception("InitReceive not called, QueueClient not initialized");
      }
      var brokkeredMessage = _queueClient.Receive(TimeSpan.FromSeconds(maxWaitSeconds));
      if (brokkeredMessage != null)
      {
        var envelope = brokkeredMessage.GetBody<MessageEnvelope<T>>();

        try
        {
          compleetAction(envelope);
          brokkeredMessage.Complete();
        }
        catch (Exception)
        {
          brokkeredMessage.Abandon();
        }

        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Receive messages from the servicebus, this should be called after @InitReceive
    /// </summary>
    /// <typeparam name="T">Type of object returned from the bus</typeparam>
    /// <param name="compleetAction">Action will contain the message</param>
    /// <param name="maxNumberOfmessages">Amount of messages to collect prior to returning in action</param>
    /// <param name="maxWaitSeconds">Time to wait until messages are received</param>  
    /// <returns></returns>
    public bool ReceiveBatch<T>(Action<IEnumerable<IMessageEnvelope<T>>> compleetAction, int maxNumberOfmessages = 1, int maxWaitSeconds = 60)
    {
      if (_subscriptionClient == null)
      {
        throw new Exception("InitReceive not called, SubscriptionClient not initialized");
      }

      var messages = _subscriptionClient.ReceiveBatch(maxNumberOfmessages, new TimeSpan(0, 0, 0, maxWaitSeconds)).ToList();

      var enveloped = messages.Select(x => x.GetBody<MessageEnvelope<T>>());
      compleetAction(enveloped);

      return messages.Any();
    }
  }
}
