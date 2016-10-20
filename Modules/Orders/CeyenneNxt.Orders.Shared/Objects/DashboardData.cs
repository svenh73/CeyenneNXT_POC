using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Objects
{
  public class DashboardData
  {
    public List<DayCount> DayCounts { get; set; }
    public int NewOrdersCount { get; set; }
    public int InProcessOrdersCount { get; set; }
  }
}