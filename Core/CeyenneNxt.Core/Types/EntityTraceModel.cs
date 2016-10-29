namespace CeyenneNxt.Core.Types
{
  public class EntityTraceModel : TraceModel
  {
    public string EntityName { get; set; }

    public int? EntityID { get; set; }

    public object ObjectToLog { get; set; }

    public override string ToString()
    {
      return $"ProcessID: {ProcessID}\tEntity: {EntityName}\tID: {EntityID}\tMessage: {Message}";
    }

    public new string ToErrorString()
    {
      return $"ProcessID: {ProcessID}\tEntity: {EntityName}\tID: {EntityID}\tMessage: {Message}\tStackTrace: {StackTrace}";
    }
  }
}