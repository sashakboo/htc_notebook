using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Calendar.Domain;

namespace Notebook.Infrastructure.Data.Config
{
  public abstract class CalendarEventsConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity>
    where TBaseEntity : CalendarEvent
  {
    public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
    {
      builder.ToTable("CalendarEvents");

      builder.Property(x => x.Subject)
        .IsRequired()
        .HasMaxLength(120);
      builder.Property(x => x.DateStart)
        .IsRequired();
      builder.Property(x => x.Done)
        .IsRequired();
    }
  }
}
