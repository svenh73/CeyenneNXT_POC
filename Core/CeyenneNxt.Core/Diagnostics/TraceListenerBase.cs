#region

using System.Diagnostics;
using System.Linq;
using CeyenneNxt.Core.Types;

#endregion

namespace CeyenneNxt.Core.Diagnostics
{
  /// <summary>
  ///   base listener overriding the functions we probably wont be changing, future listeners should only be needing to
  ///   implement @WriteLog()
  /// </summary>
  public abstract class TraceListenerBase : TraceListener
  {
    private int ProcessID { get; set; }

    public TraceListenerBase () : base()
    { }

    protected abstract void WriteLog(TraceEventType eventType, int ProcessID, TraceModel model);

    public override void TraceData(TraceEventCache eventCache, string eventSource, TraceEventType eventType, int eventID,
      params object[] data)
    {
      //expect always one

      if (data.Any())
      {
        var model = data.First() as TraceModel;

        if (model != null)
        {
          WriteLog(eventType, ProcessID, model);
        }
      }
    }

    public override void TraceEvent(TraceEventCache eventCache, string eventSource, TraceEventType eventType,
      int eventID)
    {
      TraceEvent(eventCache, eventSource, eventType, eventID, string.Empty);
    }

    public override void TraceEvent(TraceEventCache eventCache, string eventSource, TraceEventType eventType,
      int eventID, string format, params object[] arguments)
    {
      TraceEvent(eventCache, eventSource, eventType, eventID, string.Format(format, arguments));
    }

    public override void TraceEvent(TraceEventCache eventCache, string eventSource, TraceEventType eventType,
      int eventID, string message)
    {
      if (Filter == null || Filter.ShouldTrace(eventCache, eventSource, eventType, eventID, message, null, null, null))
      {
        WriteLog(eventType, ProcessID, new TraceModel {Message = message, ProcessID = eventSource});
      }
    }

    public override void Write(string message)
    {
    }

    public override void WriteLine(string message)
    {
    }

    public void SetProcessID(int id) //set id to log with
    {
      ProcessID = id;
    }
  }
}