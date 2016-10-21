namespace CeyenneNXT.Orders.ApiContracts.Models.CreateModels
{
  public class OrderLineAttributeDto
  {
    public int OrderLineID { get; set; }

    public string AttributeCode { get; set; }

    public string AttributeValue { get; set; }

    public string AttributeName { get; set; }
  }
}
