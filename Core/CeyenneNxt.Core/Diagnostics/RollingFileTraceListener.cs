using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Core.Diagnostics
{
  public sealed class RollingFileTraceListener : TraceListener
  {
    private const String DirectoryAttribute = "directory";

    public String Directory
    {
      get
      {
        return Attributes[DirectoryAttribute];
      }
      set
      {
        Attributes[DirectoryAttribute] = value;
      }
    }

    private FileStream FileStream
    {
      get;
      set;
    }

    public RollingFileTraceListener()
      : this(null)
    {
    }

    public RollingFileTraceListener(String name)
      : base(name)
    {
    }

    protected override void Dispose(Boolean disposing)
    {
      if (disposing)
      {
        FileStream.Dispose();
        FileStream = null;
      }

      base.Dispose(disposing);
    }

    protected override String[] GetSupportedAttributes()
    {
      return new[] { DirectoryAttribute };
    }

    public override void TraceData(TraceEventCache eventCache, String eventSource, TraceEventType eventType, Int32 eventID, Object data)
    {
      var message = eventType == TraceEventType.Error || eventType == TraceEventType.Critical
        ? ((TraceModel) data).ToErrorString()
        : ((TraceModel) data).ToString();
      TraceEvent(eventCache, eventSource, eventType, eventID, message);
    }

    public override void TraceData(TraceEventCache eventCache, String eventSource, TraceEventType eventType, Int32 eventID, params Object[] data)
    {
      foreach (var dataItem in data.Where(item => item != null))
      {
        TraceData(eventCache, eventSource, eventType, eventID, dataItem);
      }
    }
    
    public override void TraceEvent(TraceEventCache eventCache, String eventSource, TraceEventType eventType, Int32 eventID)
    {
      TraceEvent(eventCache, eventSource, eventType, eventID, String.Empty);
    }

    public override void TraceEvent(TraceEventCache eventCache, String eventSource, TraceEventType eventType, Int32 eventID, String message)
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendFormat("[{0:HH':'mm':'ss.fff}] {1, -16}", DateTime.Now, eventType);
      
      if (!String.IsNullOrEmpty(message))
      {
        stringBuilder.Append(message);
      }

      WriteLine(stringBuilder.ToString());
    }

    public override void TraceEvent(TraceEventCache eventCache, String eventSource, TraceEventType eventType, Int32 eventID, String format, params Object[] arguments)
    {
      TraceEvent(eventCache, eventSource, eventType, eventID, String.Format(format, arguments));
    }

    public override void Write(String message)
    {
      WriteLine(message);
    }

    public override void WriteLine(String message)
    {
      if (!String.IsNullOrEmpty(Directory) && !System.IO.Directory.Exists(Directory))
      {
        System.IO.Directory.CreateDirectory(Directory);
      }

      var fileNameBuilder = new StringBuilder();

      fileNameBuilder.AppendFormat("{0:yyyy'-'MM'-'dd}", DateTime.Now);

      if (!String.IsNullOrEmpty(Name))
      {
        fileNameBuilder.AppendFormat(" - {0}", Name);
      }

      fileNameBuilder.Append(".log");

      var fileName = Path.Combine(Path.GetFullPath(Directory), fileNameBuilder.ToString());

      if (FileStream == null || !String.Equals(FileStream.Name, fileName, StringComparison.CurrentCultureIgnoreCase))
      {
        if (FileStream != null)
        {
          FileStream.Dispose();
          FileStream = null;
        }

        FileStream = File.Open(fileName, FileMode.Append, FileAccess.Write, FileShare.Read);
      }

      var buffer = Encoding.Default.GetBytes(message + Environment.NewLine);

      FileStream.Write(buffer, 0, buffer.Length);

      FileStream.Flush();
    }
  }
}
