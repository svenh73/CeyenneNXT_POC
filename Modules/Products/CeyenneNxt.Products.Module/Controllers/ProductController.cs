﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CeyenneNxt.Orders.Module;
using CeyenneNxt.Products.Shared.Dtos;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Products.Module.Controllers
{
  //[Authorize]
  public class ProductController : ApiController, IProductApiController
  {
    public IProductModule ProductModule { get; private set; }

    public ProductController(IProductModule productModule)
    {
      ProductModule = productModule;
    }

    [HttpGet]
    [Route("api/product")]
    public List<ProductDto> Get()
    {
      using (var session = new ProductModuleSession())
      {
        return ProductModule.GetProducts(session);
      }
      
    }
    [HttpGet]
    [Route("api/product/{id}")]
    public ProductDto Get(int id)
    {
      using (var session = new ProductModuleSession())
      {
        return ProductModule.GetProducts(session).FirstOrDefault(p => p.ID == id);
      }
    }

    // POST api/values
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }
}
