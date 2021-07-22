using AutoMapper;
using Notebook.Calendar.Domain;

namespace Notebook.NHibernate
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Entities.Meeting, Meeting>().ReverseMap();
      CreateMap<Entities.Memo, Memo>().ReverseMap();
      CreateMap<Entities.Work, Work>().ReverseMap();
    }
  }
}
