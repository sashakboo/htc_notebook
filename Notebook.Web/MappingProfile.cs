using AutoMapper;
using Notebook.Calendar.Domain;
using Notebook.Contacts.Domain;
using Notebook.Web.Models.CalendarEvents;
using Notebook.Web.Models.Contacts;
using System.Linq;

namespace Notebook.Web
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CalendarEvent, CalendarEventIndexItem>()
        .ForMember(dest => dest.EventType, opt => opt.MapFrom(x =>
          x is Meeting ? CalendarEventType.Meeting :
          x is Work ? CalendarEventType.Work : 
          CalendarEventType.Memo
        ));

      CreateMap<Meeting, MeetingViewModel>().ReverseMap();
      CreateMap<Work, WorkViewModel>().ReverseMap();
      CreateMap<Memo, MemoViewModel>().ReverseMap();

      CreateMap<Contact, ContactIndexItem>();
      CreateMap<Contact, ContactViewModel>().ReverseMap();
    }
  }
}
