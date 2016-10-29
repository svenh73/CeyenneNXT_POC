namespace CeyenneNxt.Core.Types
{
  public class TraceModel
  {
    public string DataSource { get; set; }
    public string ProcessID { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public override string ToString()
    {
      return $"Domain: {ProcessID}\tMessage: {Message}";
    }

    public string ToErrorString()
    {
      return $"Domain: {ProcessID}\tMessage: {Message}\tStackTrace: {StackTrace}";
    }
  }
}