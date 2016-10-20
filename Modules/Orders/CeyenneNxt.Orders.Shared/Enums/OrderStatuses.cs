namespace CeyenneNxt.Orders.Shared.Enums
{
  /// <summary>
  /// Contains all core order statuses
  /// </summary>
  public static class OrderStatuses
  {
    public const string New = "N";
    public const string Hold = "HOLD";
    public const string Error = "ERR";
    public const string DispatchedToLogistics = "DTL";
    public const string DispatchedToFinance = "DTF";
    public const string PartiallyCancelled = "PC";
    public const string Cancelled = "C";
    public const string Shipped = "S";
    public const string PartiallyShipped = "PS";
    public const string Returned = "R";
    public const string PartiallyReturned = "PR";
    public const string Complete = "CO";
  }
}
