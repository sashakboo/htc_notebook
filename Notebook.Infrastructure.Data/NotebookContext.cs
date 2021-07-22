using System;
using Microsoft.EntityFrameworkCore;
using Notebook.Calendar.Domain;
using Notebook.Contacts.Domain;
using Notebook.Infrastructure.Data.Config;

namespace Notebook.Infrastructure.Data
{
  public class NotebookContext : DbContext
  {
    public DbSet<CalendarEvent> CalendarEvents { get; set; }

    public DbSet<Memo> Memos { get; set; }

    public DbSet<Meeting> Meetings { get; set; }

    public DbSet<Work> Works { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<ContactInfo> ContactInfos { get; set; }

    public NotebookContext(DbContextOptions<NotebookContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new MeetingConfiguration());
      modelBuilder.ApplyConfiguration(new WorkConfiguration());
      modelBuilder.ApplyConfiguration(new MemoConfiguration());
      modelBuilder.ApplyConfiguration(new ContactConfiguration());
      modelBuilder.ApplyConfiguration(new ContactInfoConfiguration());
    }
  }
}

