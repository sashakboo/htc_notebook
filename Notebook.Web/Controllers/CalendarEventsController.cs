using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Infrastructure.Data;
using Notebook.Web.Models.CalendarEvents;
using Notebook.Calendar.Domain.CalendarEvents;

namespace Notebook.Web.Controllers
{
  public class CalendarEventsController : Controller
  {
    private readonly NotebookContext _context;

    private readonly IMapper _mapper;

    public CalendarEventsController(NotebookContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalendarEventIndexItem>>> GetFiltered(
      DateTime? from, DateTime? to, CalendarEventType? eventType, string text)
    {
      var events = _context.CalendarEvents.AsNoTracking().AsQueryable();

      if (from.HasValue)
        events = events.Where(x => x.DateStart >= from);
      if (to.HasValue)
        events = events.Where(x => x.DateStart <= to);
      if (eventType.HasValue)
      {
        switch (eventType.Value)
        {
          case CalendarEventType.Meeting:
            events = events.Where(x => x is Meeting);
            break;
          case CalendarEventType.Memo:
            events = events.Where(x => x is Memo);
            break;
          case CalendarEventType.Work:
            events = events.Where(x => x is Work);
            break;
          default:
            break;
        }
      }

      if (!string.IsNullOrWhiteSpace(text))
      {
        events = events.Where(x => 
                              x.Subject.Contains(text) || 
                              (x is Meeting
                                && (x as Meeting).Place.Contains(text)));
      }

      return await events.Select(x => _mapper.Map<CalendarEventIndexItem>(x)).ToListAsync();
    }

    [HttpGet]
    public IActionResult CreateMeeting()
    {
      return PartialView();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMeeting([FromForm] MeetingViewModel meeting)
    {
      if (ModelState.IsValid)
      {
        _context.Add(_mapper.Map<Meeting>(meeting));
        await _context.SaveChangesAsync();
        return Ok(meeting);
      }
      return BadRequest(ModelState);
    }

    [HttpGet]
    public IActionResult CreateMemo()
    {
      return PartialView();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMemo([FromForm] MemoViewModel memo)
    {
      if (ModelState.IsValid)
      {
        _context.Add(_mapper.Map<Memo>(memo));
        await _context.SaveChangesAsync();
        return Ok(memo);
      }
      return BadRequest(ModelState);
    }

    [HttpGet]
    public IActionResult CreateWork()
    {
      return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> CreateWork([FromForm] WorkViewModel work)
    {
      if (ModelState.IsValid)
      {
        _context.Add(_mapper.Map<Work>(work));
        await _context.SaveChangesAsync();
        return Ok(work);
      }
      return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<IActionResult> EditMeeting(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var meeting = await _context.Meetings.FindAsync(id);
      if (meeting == null)
      {
        return NotFound();
      }
      return PartialView(_mapper.Map<MeetingViewModel>(meeting));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMeeting(int id, [FromForm] MeetingViewModel meeting)
    {
      if (id != meeting.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(_mapper.Map<Meeting>(meeting));
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!_context.Meetings.Any(e => e.Id == id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return Ok();
      }
      return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<IActionResult> EditMemo(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var memo = await _context.Memos.FindAsync(id);
      if (memo == null)
      {
        return NotFound();
      }
      return PartialView(_mapper.Map<MemoViewModel>(memo));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMemo(int id, [FromForm] MemoViewModel memo)
    {
      if (id != memo.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(_mapper.Map<Memo>(memo));
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!_context.Memos.Any(e => e.Id == id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return Ok(memo);
      }
      return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<IActionResult> EditWork(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var work = await _context.Works.FindAsync(id);
      if (work == null)
      {
        return NotFound();
      }
      return PartialView(_mapper.Map<WorkViewModel>(work));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditWork(int id, [FromForm] WorkViewModel work)
    {
      if (id != work.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(_mapper.Map<Work>(work));
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!_context.Works.Any(e => e.Id == id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return Ok(work);
      }
      return BadRequest(ModelState);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      var calendarEvent = await _context.CalendarEvents.FindAsync(id);
      _context.CalendarEvents.Remove(calendarEvent);
      await _context.SaveChangesAsync();
      return Ok();
    }
  }
}
