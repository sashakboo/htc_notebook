namespace Notebook.Contacts.Domain
{
  public class ContactInfo
  {
    public int Id { get; set; }

    public Contact Contact { get; set; }
    
    public ContactInfoType ContactType { get; set; }

    public string Value { get; set; }
  }
}
