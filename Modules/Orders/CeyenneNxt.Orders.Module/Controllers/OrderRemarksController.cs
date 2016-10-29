using System.Collections.Generic;
using System.Web.Http;

namespace CeyenneNxt.Orders.Module.Controllers
{
    public class OrderRemarksController : ApiController
    {
        private IOrderRemarkRepository orderRemarkRepository;

        public OrderRemarksController(IOrderRemarkRepository orderRemarkRepository) : base()
        {
            this.orderRemarkRepository = orderRemarkRepository;
        }

        // POST: api/OrderRemarks
        /// <summary>
        /// Remark create
        /// </summary>
        /// <param name="remark"></param>
        public void Post([FromBody]OrderRemark remark)
        {
            this.orderRemarkRepository.Add(remark);
        }

        [HttpGet]
        [Route("api/orderRemarks/getByOrderId")]
        public IList<OrderRemark> GetByOrderId(int orderId)
        {
            IList<OrderRemark> orderRemarks = this.orderRemarkRepository.GetOrderRemarksByOrderId(orderId);
            return orderRemarks;
        }


        //// GET: api/Remarks
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Remarks/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Remarks
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Remarks/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Remarks/5
        //public void Delete(int id)
        //{
        //}
    }
}
