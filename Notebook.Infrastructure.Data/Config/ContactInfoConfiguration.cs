using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Contacts.Domain;

namespace Notebook.Infrastructure.Data.Config
{
  class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
  {
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
      builder.ToTable("ContactInfo");

      builder.Property(x => x.ContactType)
        .IsRequired();
      builder.Property(x => x.Value)
        .IsRequired();

      builder.HasOne(x => x.Contact)
        .WithMany(x => x.ContactInformation);
    }
  }
}
