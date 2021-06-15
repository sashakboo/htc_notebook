using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Contacts.Domain;
using ContactViewModels = Notebook.Web.Models.Contacts;
using Notebook.Infrastructure.Data;

namespace Notebook.Web.Controllers
{
  public class ContactsController : Controller
  {
    private readonly NotebookContext _context;

    public ContactsController(NotebookContext context)
    {
      _context = context;
    }

    // GET: Contacts
    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var contacts = _context.Contacts.Select(x => Utils.Mapper.ToContactIndexItem(x));

      return View(await _context.Contacts
        .Select(x => Utils.Mapper.ToContactIndexItem(x))
        .ToListAsync());
    }

    // GET: Contacts/Create
    [HttpGet]
    public IActionResult Create()
    {
      return PartialView();
    }

    // POST: Contacts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ContactViewModels.ContactViewModel contact)
    {
      if (ModelState.IsValid)
      {
        var contactModel = new Contact()
        {
          Id = contact.Id,
          Name = contact.Name,
          MiddleName = contact.MiddleName,
          Surname = contact.Surname,
          Birthday = contact.Birthday,
          Company = contact.Company,
          Position = contact.Position          
        };
        _context.Add(contactModel);
        await _context.SaveChangesAsync();
        return Ok(contact);
      }
      return BadRequest(ModelState);
    }
  }
}
