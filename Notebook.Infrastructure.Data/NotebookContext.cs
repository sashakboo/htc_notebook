using System;
using Microsoft.EntityFrameworkCore;
using Notebook.Calendar.Domain.CalendarEvents;
using Notebook.Contacts.Domain;

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
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      #region CalendarEvents
      modelBuilder.Entity<CalendarEvent>().Property(x => x.Subject)
        .IsRequired()
        .HasMaxLength(120);
      modelBuilder.Entity<CalendarEvent>().Property(x => x.DateStart)
        .IsRequired();
      modelBuilder.Entity<CalendarEvent>().Property(x => x.Done)
        .IsRequired();
      modelBuilder.Entity<Meeting>().Property(x => x.DateEnd)
        .IsRequired();
      modelBuilder.Entity<Meeting>().Property(x => x.Place)
        .IsRequired()
        .HasMaxLength(120);
     modelBuilder.Entity<Work>().Property(x => x.DateEnd)
        .IsRequired();
      #endregion

      #region Contacts
      modelBuilder.Entity<Contact>().Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(50);
      modelBuilder.Entity<Contact>().Property(x => x.MiddleName)
        .HasMaxLength(50);
      modelBuilder.Entity<Contact>().Property(x => x.Surname)
        .HasMaxLength(50);
      modelBuilder.Entity<Contact>().Property(x => x.Company)
        .HasMaxLength(50);
      modelBuilder.Entity<Contact>().Property(x => x.Position)
        .HasMaxLength(50);
      #endregion
    }
  }
}
