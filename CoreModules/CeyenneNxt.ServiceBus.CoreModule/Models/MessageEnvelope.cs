using System;
using System.Runtime.Serialization;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace CeyenneNxt.ServiceBus.CoreModule.Models
{
  [DataContract]
  public class MessageEnvelope<T> : IMessageEnvelope<T>
  {
    private BrokeredMessage Message { get; set; }

    [DataMember]
    public string DataSource { get; set; }
    [DataMember]
    public MessageTypeAction MessageTypeAction { get; set; }
    [DataMember]
    public T Item { get; set; }
    [DataMember]
    public DateTime DateCreated { get; set; }

    public void Abandon()
    {
      Message.Abandon();
    }

    public void Deadletter(string reason = null,string error = null)
    {
      Message.DeadLetter(reason,error);
    }

    public void Complete()
    {
      Message.Complete();
    }

    public IMessageEnvelope<T> CreateMessage(T item, MessageTypeAction action, string dataSource)
    {
      return new MessageEnvelope<T>
      {
        Item = item,
        DateCreated = DateTime.Now,
        MessageTypeAction = action,
        DataSource = dataSource
      };
    }

  }
}
