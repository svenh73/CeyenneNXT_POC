using System;
using System.Collections.Generic;

namespace CeyenneNxt.Core.ServiceBus
{
  public interface IServiceBusModule
  {
    void Dispose();
    void InitReceiveQueue(string queueName);

    IMessageEnvelope<TDto> CreateEnvelope<TDto>();
    void InitReceiveTopic(string topicName, string subscriptionName, string sqlFilter = null);
    void PublishToQueue<T>(IMessageEnvelope<T> item, string queueName, string vendorName = null, string channelName = null);
    void PublishToTopic<T>(IMessageEnvelope<T> item, string topicName, string vendorName = null, string channelName = null);
    bool ReceiveBatch<T>(Action<IEnumerable<IMessageEnvelope<T>>> compleetAction, int maxNumberOfmessages = 1, int maxWaitSeconds = 60);
    bool ReceiveFromQueue<T>(Action<IMessageEnvelope<T>> compleetAction, int maxWaitSeconds = 60);
    bool ReceiveFromTopic<T>(Action<IMessageEnvelope<T>> compleetAction, int maxWaitSeconds = 60);
  }
}
