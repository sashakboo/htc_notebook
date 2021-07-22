using System.Threading.Tasks;

namespace Notebook.Calendar.Domain
{
  public interface ICalendarEventsRepository
  {
    public Task<CalendarEvent> Get(int id);

  }
}
