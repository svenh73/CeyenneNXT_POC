namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class RefundSearchResultDto
  {
    public int ID { get; set; }
    public int CustomerID { get; set; }
    public int OrderID { get; set; }
    public string OrderBackendID { get; set; }
    public string CustomerName { get; set; }
    public string CurrencyCode { get; set; }
    public string ReturnCode { get; set; }
    public string PaymentMethod { get; set; }
    public string Invoice { get; set; }
    public decimal? Amount { get; set; }
    public string Description { get; set; }
  }
}
