using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Calendar.Domain;

namespace Notebook.NHibernate
{
  public class CalendarRepository : ICalendarEventsRepository
  {
    public Task<CalendarEvent> Get(int id)
    {
      throw new NotImplementedException();
    }

    public CalendarRepository()
    {

    }
  }
}
