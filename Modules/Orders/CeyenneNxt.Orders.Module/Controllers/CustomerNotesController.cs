using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class CustomerNotesController : BaseController
  {
    private readonly CustomerNoteModule _customerNoteModule;
    private readonly UserModule _userModule;
    private readonly MapperConfiguration _config;

    public CustomerNotesController()
    {
      _customerNoteModule = new CustomerNoteModule();
      _userModule = new UserModule();
      _config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Core.Objects.CustomerNoteSearchResult, CustomerNoteSearchResult>();
        cfg.CreateMap<CustomerNote, Core.Entities.CustomerNote>();
      });
    }

    public void Post([FromBody] CustomerNote noteContract)
    {
      if (noteContract == null)
      {
        throw new NullReferenceException();
      }

      var mapper = _config.CreateMapper();
      var note = mapper.Map<Core.Entities.CustomerNote>(noteContract);
      var userName = GetUserName();
      if (!string.IsNullOrWhiteSpace(userName))
      {
        var user = _userModule.GetByUserName(userName);
        if (user != null)
        {
          note.UserID = user.ID;
        }
      }
      _customerNoteModule.InsertNote(note);
    }

    [HttpGet]
    [Route("api/customerNotes/search/{customerID}")]
    public List<CustomerNoteSearchResult> Search(int customerID)
    {
      var mapper = _config.CreateMapper();
      var notes = _customerNoteModule.GetByCustomerID(customerID);
      var notesCostract = mapper.Map<List<CustomerNoteSearchResult>>(notes);
      return notesCostract;
    }
  }
}
