﻿using System;

namespace Notebook.Calendar.Domain
{
  public class Meeting : CalendarEvent
  {
    public DateTime DateEnd { get; set; }

    public string Place { get; set; }
  }
}
