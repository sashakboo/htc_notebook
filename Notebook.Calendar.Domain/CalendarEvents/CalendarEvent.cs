using System;

namespace Notebook.Calendar.Domain.CalendarEvents
{
  public abstract class CalendarEvent
  {
    public virtual int Id { get; set; }

    public virtual string Subject { get; set; }

    public virtual DateTime DateStart { get; set; }

    public virtual bool Done { get; set; }
  }
}
