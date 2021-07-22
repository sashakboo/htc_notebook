using System;

namespace Notebook.NHibernate.Entities
{
  public class Meeting : CalendarEvent
  {
    public DateTime DateEnd { get; set; }

    public string Place { get; set; }
  }
}
