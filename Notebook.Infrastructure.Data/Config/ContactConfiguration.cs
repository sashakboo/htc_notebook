using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Contacts.Domain;

namespace Notebook.Infrastructure.Data.Config
{
  class ContactConfiguration : IEntityTypeConfiguration<Contact>
  {
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
      builder.ToTable("Contacts");

      builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(50);
      builder.Property(x => x.MiddleName)
        .HasMaxLength(50);
      builder.Property(x => x.Surname)
        .HasMaxLength(50);
      builder.Property(x => x.Company)
        .HasMaxLength(50);
      builder.Property(x => x.Position)
        .HasMaxLength(50);
    }
  }
}
