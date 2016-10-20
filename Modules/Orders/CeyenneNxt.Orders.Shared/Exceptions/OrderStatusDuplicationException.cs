using System;

namespace CeyenneNxt.Orders.Shared.Exceptions
{

  [Serializable]
  public class OrderStatusDuplicationException : System.Exception
  {
    public OrderStatusDuplicationException() { }
    public OrderStatusDuplicationException(string message) : base(message) { }
    public OrderStatusDuplicationException(string message, System.Exception inner) : base(message, inner) { }
    protected OrderStatusDuplicationException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
