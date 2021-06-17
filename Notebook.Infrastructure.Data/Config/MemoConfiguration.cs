using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Calendar.Domain.CalendarEvents;

namespace Notebook.Infrastructure.Data.Config
{
  public class MemoConfiguration : CalendarEventsConfiguration<Memo>
  {
    public override void Configure(EntityTypeBuilder<Memo> builder)
    {
      base.Configure(builder);
    }
  }
}
