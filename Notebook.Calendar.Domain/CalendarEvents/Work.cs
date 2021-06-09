using System;

namespace Notebook.Calendar.Domain.CalendarEvents
{
  public class Work : CalendarEvent
  {
    public DateTime DateEnd { get; set; }
  }
}