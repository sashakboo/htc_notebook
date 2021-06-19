using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Infrastructure.Data;
using Notebook.Web.Models.Contacts;
using Notebook.Contacts.Domain;

namespace Notebook.Web.Controllers
{
  public class ContactsController : Controller
  {
    private readonly NotebookContext _context;

    private readonly IMapper _mapper;

    public ContactsController(NotebookContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      return View(await _context.Contacts
        .Select(x => _mapper.Map<ContactIndexItem>(x))
        .ToListAsync());
    }

    [HttpGet]
    public IActionResult Create()
    {
      return PartialView();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ContactViewModel contact)
    {
      if (ModelState.IsValid)
      {
        _context.Add(_mapper.Map<Contact>(contact));
        await _context.SaveChangesAsync();
        return Ok(contact);
      }
      return BadRequest(ModelState);
    }
  }
}
