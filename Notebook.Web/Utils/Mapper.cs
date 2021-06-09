using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels = Notebook.Web.Models;
using CalendarModels = Notebook.Calendar.Domain.CalendarEvents;
using ContactModels = Notebook.Contacts.Domain;

namespace Notebook.Web.Utils
{
  public static class Mapper
  {
    public static ViewModels.CalendarEvents.CalendarEventIndexItem ToCalendarEventViewModel(CalendarModels.CalendarEvent item) 
    {
      return new ViewModels.CalendarEvents.CalendarEventIndexItem()
      {
        Id = item.Id,
        Subject = item.Subject,
        DateStart = item.DateStart,
        DateEnd = item switch 
        {
          CalendarModels.Meeting e => e.DateEnd,
          CalendarModels.Work e => e.DateEnd,
          _ => null
        },
        Done = item.Done,
        EventType = item switch
        {
          CalendarModels.Meeting _ => ViewModels.CalendarEvents.CalendarEventType.Meeting,
          CalendarModels.Work _ => ViewModels.CalendarEvents.CalendarEventType.Work,
          _ => ViewModels.CalendarEvents.CalendarEventType.Memo
        }
      };
    }

    public static ViewModels.CalendarEvents.MeetingViewModel ToMeetingViewModel(CalendarModels.Meeting item)
    {
      return new ViewModels.CalendarEvents.MeetingViewModel()
      {
        Id = item.Id,
        Subject = item.Subject,
        DateStart = item.DateStart,
        DateEnd = item.DateEnd,
        Place = item.Place,
        Done = item.Done
      };
    }

    public static ViewModels.Contacts.ContactIndexItem ToContactIndexItem(Contacts.Domain.Contact contact)
    {
      return new ViewModels.Contacts.ContactIndexItem()
      {
        Id = contact.Id,
        Surname = contact.Surname,
        Name = contact.Name,
        MiddleName = contact.MiddleName,
        Birthday = contact.Birthday,
        Company = contact.Company,
        Position = contact.Position,
        ContactInfo = contact.ContactInformation?.FirstOrDefault()?.Value
      };
    }
  }
}
