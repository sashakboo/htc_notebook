using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Calendar.Domain.CalendarEvents;

namespace Notebook.Infrastructure.Data.Config
{
  public class MeetingConfiguration: CalendarEventsConfiguration<Meeting>
  {
    public override void Configure(EntityTypeBuilder<Meeting> builder)
    {
      builder.Property(x => x.DateEnd)
        .IsRequired();
      builder.Property(x => x.Place)
        .IsRequired()
        .HasMaxLength(120);

      base.Configure(builder);
    }
  }
}
