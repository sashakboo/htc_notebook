using System;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Web.Models.Contacts
{
  public class ContactIndexItem
  {
    public int Id { get; set; }

    [Display(Name = "Фамилия")]
    public string Surname { get; set; }

    [Display(Name = "Имя")]
    public string Name { get; set; }

    [Display(Name = "Отчество")]
    public string MiddleName { get; set; }

    [Display(Name = "Дата рождения")]
    public DateTime Birthday { get; set; }

    [Display(Name = "Компания")]
    public string Company { get; set; }

    [Display(Name = "Должность")]
    public string Position { get; set; }

    [Display(Name = "Информация")]
    public string ContactInfo { get; set; }
  }
}
