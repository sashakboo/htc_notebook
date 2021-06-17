using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Calendar.Domain.CalendarEvents;

namespace Notebook.Infrastructure.Data.Config
{
  public class WorkConfiguration : CalendarEventsConfiguration<Work>
  {
    public override void Configure(EntityTypeBuilder<Work> builder)
    {
      builder.Property(x => x.DateEnd)
        .IsRequired();

      base.Configure(builder);
    }
  }
}
