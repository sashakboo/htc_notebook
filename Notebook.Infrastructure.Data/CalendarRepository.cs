using System.Linq;
using System.Collections.Generic;
using Notebook.Calendar.Domain;
using System.Threading.Tasks;

namespace Notebook.Infrastructure.Data
{
  public class CalendarRepository : ICalendarEventsRepository
  {
    private readonly NotebookContext _context;

    public async Task<CalendarEvent> Get(int id)
    {
      return await _context.CalendarEvents.FindAsync(id);
    }

    public CalendarRepository(NotebookContext context)
    {
      _context = context;
    }
  }
}
