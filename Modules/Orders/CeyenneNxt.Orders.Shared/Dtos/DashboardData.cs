using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class DashboardDataDto
  {
    public List<DayCountDto> DayCounts { get; set; }
    public int NewOrdersCount { get; set; }
    public int InProcessOrdersCount { get; set; }
  }
}