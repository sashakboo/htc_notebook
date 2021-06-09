using System;

namespace Notebook.Calendar.Domain.CalendarEvents
{
  public class Meeting : CalendarEvent
  {
    public DateTime DateEnd { get; set; }

    public string Place { get; set; }
  }
}
