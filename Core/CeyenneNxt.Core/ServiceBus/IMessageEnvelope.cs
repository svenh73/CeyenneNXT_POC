using System;
using System.Runtime.Serialization;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.ServiceBus
{
  public interface IMessageEnvelope<T>
  {
    [DataMember]
    string DataSource { get; set; }
    [DataMember]
    MessageTypeAction MessageTypeAction { get; set; }
    [DataMember]
    T Item { get; set; }
    [DataMember]
    DateTime DateCreated { get; set; }
    void Abandon();
    void Deadletter(string reason = null, string error = null);
    void Complete();
    IMessageEnvelope<T> CreateMessage(T item, MessageTypeAction action, string dataSource);
  }
}