namespace CeyenneNxt.Orders.Shared.Constants
{
  public static class Constants
  {
    public const string CONNECTION_STRING_NAME = "CNXTOrders";



    public static class StoredProcedures
    {
      public static class Countries
      {
        public const string GetByCode = "Country_SelectByCode";

        public const string GetByID = "Country_Select";

        public const string Insert = "Country_Insert";
      }

      public static class Orders
      {
        public const string GetByNotDispatched = "Order_GetNotDispatched";

        public const string GetByIdDetails = "Order_Select_Details";

        public const string GetByID = "Order_Select";

        public const string GetIDByBackendID = "Order_GetIDByBackendID";

        public const string OrderSearch = "Order_Search";

        public const string Create = "Order_Insert";

        public const string UpdateHoldStatus = "Order_UpdateHoldStatus";

        public const string SetOrderDispatched = "Order_SetDispatched";

        public const string GetDashboardData = "Order_GetDashboardData";

        public const string GetAllOrderTypes = "OrderType_SelectAll";

        public const string GetBetweenStatuses = "Order_GetBetweenStatuses";

        public const string GetByLatestStatus = "Order_GetByLatestStatus";
      }

      public static class OrderLines
      {
        public const string Create = "OrderLine_Insert";

        public const string GetByID = "OrderLine_Select";

        public const string GetByIdDetails = "OrderLine_Select_Details";
      }

      public static class OrderLineStatus
      {
        public const string GetAllStatuses = "OrderLineStatus_SelectAll";

        public const string GetIDByCode = "OrderLineStatus_SelectByCode";
      }

      public static class OrderLineStatusHistory
      {
        public const string CreateStatusHistory = "OrderLineStatusHistory_Insert";

        public const string GetStatusHistoryByOrderLineID = "OrderLineStatusHistory_SelectByOrderLineID";
      }

      public static class OrderAttributes
      {
        public const string GetIDByCode = "OrderAttribute_GetIDByCode";

        public const string Create = "OrderAttribute_Insert";
      }

      public static class OrderLineAttributes
      {
        public const string GetIDByCode = "OrderLineAttribute_GetIDByCode";

        public const string Create = "OrderLineAttribute_Insert";

        public const string GetAttributes = "OrderLineAttributeValue_SelectByOrderLineID";
      }

      public static class OrderLineAttributeValue
      {
        public const string Create = "OrderLineAttributeValue_Insert";
      }

      public static class OrderAttributeValue
      {
        public const string Create = "OrderAttributeValue_Insert";
      }

      public static class Customers
      {
        public const string GetByBackendID = "Customer_SelectByBackendID";

        public const string GetByID = "Customer_Select";

        public const string Create = "Customer_Insert";

        public const string CustomerSearch = "Customer_Search";

        public const string SelectWithAddressesAndOrders = "Customer_SelectWithAddressesAndOrders";
      }

      public static class Users
      {
        public const string GetByUserName = "User_GetByUserName";

        public const string GetByID = "User_GetByID";

        public const string Create = "User_Insert";
      }

      public static class ReturnCodes
      {
        public const string SelectAll = "ReturnCode_SelectAll";
      }

      public static class PaymentMethods
      {
        public const string SelectAll = "PaymentMethod_SelectAll";
      }

      public static class RefundStatuses
      {
        public const string SelectAll = "RefundStatus_SelectAll";
      }

      public static class Currencies
      {
        public const string SelectAll = "Currency_SelectAll";
      }

      public static class Refunds
      {
        public const string GetByOrderID = "Refund_GetByOrderID";

        public const string Create = "Refund_Insert";

        public const string Update = "Refund_Update";

        public const string Search = "Refund_Search";
      }

      public static class CustomerNotes
      {
        public const string Create = "CustomerNote_Insert";
        public const string SelectByCustomerID = "CustomerNote_SelectByCustomerID";
      }

      public static class OrderNotes
      {
        public const string Create = "OrderNote_Insert";
        public const string SelectByOrderID = "OrderNote_SelectByOrderID";
      }

      public static class OrderUpdateMessage
      {
        public const string GetNotProcessed = "OrderUpdateMessage_SelectNotProcessed";

        public const string Create = "OrderUpdateMessage_Insert";

        public const string Toggle = "OrderUpdateMessage_ToggleProcessed";
      }

      public static class OrderUpdateMessageLine
      {
        public const string GetByUpdateMessageID = "OrderUpdateMessageLine_GetByOrderUpdateMessageID";

        public const string Create = "OrderUpdateMessageLine_Insert";
      }

      public static class OrderAddress
      {
        public const string Create = "OrderAddress_Insert";
      }

      public static class Address
      {
        public const string GetByBackendID = "CustomerAddress_SelectByBackendID";

        public const string GetByCustomerAndBackendID = "CustomerAddress_SelectByBackendIDAndCustomerID";

        public const string Create = "CustomerAddress_Insert";

        public const string GetByID = "CustomerAddress_Select";
      }

      public static class AddressType
      {
        public const string GetByCode = "CustomerAddressType_SelectByCode";

        public const string GetByID = "CustomerAddressType_Select";

        public const string Insert = "CustomerAddressType_Insert";
      }

      public static class OrderStatus
      {
        public const string SelectAll = "OrderStatus_SelectAll";

        public const string GetIDByCode = "OrderStatus_SelectByCode";

        public const string Create = "OrderStatusHistory_Insert";

        public const string GetStatusHistory = "OrderStatusHistory_Select";

        public const string GetStatusHistoryByOrderID = "OrderStatusHistory_SelectByOrderID";

        public const string GetByID = "OrderStatusHistory_SelectByID";

      }

      public static class OrderType
      {
        public const string GetByName = "OrderType_SelectByName";

        public const string Create = "OrderType_Insert";
      }

      public static class RefundStatusHistory
      {
        public const string Create = "RefundStatusHistory_Insert";

        public const string SelectByRefundID = "RefundStatusHistory_SelectByRefundID";
      }

      public static class OrderQuantityUnit
      {
        public const string GetIDByCode = "OrderQuantityUnit_GetIDByCode";
        public const string Create = "OrderQuantityUnit_Insert";
      }
    }

    public static int DefaultPageSize = 100;
  }
}