using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notebook.Infrastructure.Data;
using CalendarEventsModel = Notebook.Calendar.Domain.CalendarEvents;
using Notebook.Web.Models.CalendarEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Notebook.Web.Controllers
{
  public class CalendarEventsController : Controller
  {
    private NotebookContext dbContext;

    public CalendarEventsController(NotebookContext context)
    {
      dbContext = context;
    }

    public ActionResult Index()
    {
      return View();
    }

    // GET: CalendarEvents
    public async Task<ActionResult<IEnumerable<CalendarEventIndexItem>>> GetFiltered(DateTime? from, DateTime? to, CalendarEventType? eventType, string text)
    {
      var events = dbContext.CalendarEvents.AsNoTracking().AsQueryable();

      if (from.HasValue)
        events = events.Where(x => x.DateStart >= from);
      if (to.HasValue)
        events = events.Where(x => x.DateStart <= to);
      if (eventType.HasValue)
      {
        switch (eventType.Value)
        {
          case CalendarEventType.Meeting:
            events = events.Where(x => x is CalendarEventsModel.Meeting);
            break;
          case CalendarEventType.Memo:
            events = events.Where(x => x is CalendarEventsModel.Memo);
            break;
          case CalendarEventType.Work:
            events = events.Where(x => x is CalendarEventsModel.Work);
            break;
          default:
            break;
        }
      }

      if (!string.IsNullOrWhiteSpace(text))
      {
        events = events.Where(x => 
                              x.Subject.Contains(text) || 
                              (x is CalendarEventsModel.Meeting
                                && (x as CalendarEventsModel.Meeting).Place.Contains(text)));
      }

      return await events.Select(x => Utils.Mapper.ToCalendarEventViewModel(x)).ToListAsync();
    }

    // GET: CalendarEvents/CreateMeeting
    public IActionResult CreateMeeting()
    {
      return PartialView();
    }

    // POST: CalendarEvents/CreateMeeting
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMeeting([FromForm] MeetingViewModel meeting)
    {
      if (ModelState.IsValid)
      {
        var meetingModel = new CalendarEventsModel.Meeting() 
        {
          DateStart = meeting.DateStart,
          DateEnd = meeting.DateEnd,
          Subject = meeting.Subject,
          Place = meeting.Place,
          Done = meeting.Done
        };

        dbContext.Add(meetingModel);
        await dbContext.SaveChangesAsync();
        return Ok(meetingModel);
      }
      return BadRequest(ModelState);
    }

    // GET: CalendarEvents/CreateMemo
    public IActionResult CreateMemo()
    {
      return PartialView();
    }

    // POST: CalendarEvents/CreateMemo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMemo([FromForm] MemoViewModel memo)
    {
      if (ModelState.IsValid)
      {
        var memoModel = new CalendarEventsModel.Memo()
        {
          DateStart = memo.DateStart,
          Subject = memo.Subject,
          Done = memo.Done
        };

        dbContext.Add(memoModel);
        await dbContext.SaveChangesAsync();
        return Ok(memoModel);
      }
      return BadRequest(ModelState);
    }

    // GET: CalendarEvents/CreateWork
    public IActionResult CreateWork()
    {
      return PartialView();
    }

    // POST: CalendarEvents/CreateWork
    [HttpPost]
    public async Task<IActionResult> CreateWork([FromForm] WorkViewModel work)
    {
      if (ModelState.IsValid)
      {
        var workModel = new CalendarEventsModel.Work()
        {
          DateStart = work.DateStart,
          DateEnd = work.DateEnd,
          Subject = work.Subject,
          Done = work.Done
        };

        dbContext.Add(workModel);
        await dbContext.SaveChangesAsync();
        return Ok(workModel);
      }
      return BadRequest(ModelState);
    }

    // GET: CalendarEvents/EditMeeting/5
    public async Task<IActionResult> EditMeeting(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var meeting = await dbContext.Meetings.FindAsync(id);
      if (meeting == null)
      {
        return NotFound();
      }
      return PartialView(new MeetingViewModel() { 
        Id = meeting.Id,
        DateStart = meeting.DateStart,
        DateEnd = meeting.DateEnd,
        Subject = meeting.Subject,
        Place = meeting.Place,
        Done = meeting.Done
      });
    }

    // POST: CalendarEvents/EditMeeting/5
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
          var meetingModel = new CalendarEventsModel.Meeting()
          {
            Id = meeting.Id,
            DateStart = meeting.DateStart,
            DateEnd = meeting.DateEnd,
            Subject = meeting.Subject,
            Place = meeting.Place,
            Done = meeting.Done
          };

          dbContext.Update(meetingModel);
          await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!dbContext.Meetings.Any(e => e.Id == id))
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

    // GET: CalendarEvents/EditMemo/5
    public async Task<IActionResult> EditMemo(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var memo = await dbContext.Memos.FindAsync(id);
      if (memo == null)
      {
        return NotFound();
      }
      return PartialView(new MemoViewModel()
      {
        Id = memo.Id,
        DateStart = memo.DateStart,
        Subject = memo.Subject,
        Done = memo.Done
      });
    }

    // POST: CalendarEvents/EditMemo/5
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
          var memoModel = new CalendarEventsModel.Meeting()
          {
            Id = memo.Id,
            DateStart = memo.DateStart,
            Subject = memo.Subject,
            Done = memo.Done
          };

          dbContext.Update(memoModel);
          await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!dbContext.Memos.Any(e => e.Id == id))
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

    // GET: CalendarEvents/EditWork/5
    public async Task<IActionResult> EditWork(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var work = await dbContext.Works.FindAsync(id);
      if (work == null)
      {
        return NotFound();
      }
      return PartialView(new WorkViewModel()
      {
        Id = work.Id,
        DateStart = work.DateStart,
        DateEnd = work.DateEnd,
        Subject = work.Subject,
        Done = work.Done
      });
    }

    // POST: CalendarEvents/EditMemo/5
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
          var workModel = new CalendarEventsModel.Work()
          {
            Id = work.Id,
            DateStart = work.DateStart,
            DateEnd = work.DateEnd,
            Subject = work.Subject,
            Done = work.Done
          };

          dbContext.Update(workModel);
          await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!dbContext.Works.Any(e => e.Id == id))
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

    // POST: CalendarEvents/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
      var calendarEvent = await dbContext.CalendarEvents.FindAsync(id);
      dbContext.CalendarEvents.Remove(calendarEvent);
      await dbContext.SaveChangesAsync();
      return Ok();
    }
  }
}
