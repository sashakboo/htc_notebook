using System;
using System.Collections.Generic;

namespace Notebook.Contacts.Domain
{
  public class Contact
  {
    public int Id { get; set; }

    public string Surname { get; set; }

    public string Name { get; set; }

    public string MiddleName { get; set; }

    public DateTime Birthday { get; set; }

    public string Company { get; set; }

    public string Position { get; set; }

    public ICollection<ContactInfo> ContactInformation { get; set; }
  }
}
