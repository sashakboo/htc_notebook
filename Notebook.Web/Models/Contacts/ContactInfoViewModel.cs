using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Web.Models.Contacts
{
  public class ContactInfoViewModel
  {
    [Required(ErrorMessage = "Не указан тип контакта")]
    [Display(Name = "Тип")]
    public ContactInfoType ContactType { get; set; }

    [Required(ErrorMessage = "Не указано значение")]
    [Display(Name = "Значение")]
    public string Value { get; set; }
  }
}
