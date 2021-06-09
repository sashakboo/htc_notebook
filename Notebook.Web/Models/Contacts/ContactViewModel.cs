using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Notebook.Web.Models.Contacts
{
  public class ContactViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Фамилия")]
    public string Surname { get; set; }

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Не заполнено имя")]
    public string Name { get; set; }

    [Display(Name = "Фамилия")]
    public string MiddleName { get; set; }

    [Display(Name = "Дата рождения")]
    public DateTime Birthday { get; set; }

    [Display(Name = "Компания")]
    public string Company { get; set; }

    [Display(Name = "Должность")]
    public string Position { get; set; }

    [Display(Name = "Контакты")]
    [Required(ErrorMessage = "Добавьте хотя бы один контакт")]
    public ICollection<ContactInfoViewModel> ContactInformation { get; set; }
  }
}
