using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class OrderNotesController : BaseController
  {
    private readonly OrderNoteModule _orderNoteModule;
    private readonly UserModule _userModule;
    private readonly MapperConfiguration _config;

    public OrderNotesController()
    {
      _orderNoteModule = new OrderNoteModule();
      _userModule = new UserModule();
      _config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Core.Objects.OrderNoteSearchResult, OrderNoteSearchResult>();
        cfg.CreateMap<OrderNote, Core.Entities.OrderNote>();
      });
    }

    public void Post([FromBody] OrderNote noteContract)
    {
      if (noteContract == null)
      {
        throw new NullReferenceException();
      }

      var mapper = _config.CreateMapper();
      var note = mapper.Map<Core.Entities.OrderNote>(noteContract);
      var userName = GetUserName();
      if (!string.IsNullOrWhiteSpace(userName))
      {
        var user = _userModule.GetByUserName(userName);
        if (user != null)
        {
          note.UserID = user.ID;
        }
      }
      _orderNoteModule.InsertNote(note);
    }

    [HttpGet]
    [Route("api/orderNotes/search/{orderID}")]
    public List<OrderNoteSearchResult> Search(int orderID)
    {
      var mapper = _config.CreateMapper();
      var notes = _orderNoteModule.GetByOrderID(orderID);
      var notesCostract = mapper.Map<List<OrderNoteSearchResult>>(notes);
      return notesCostract;
    }
  }
}
