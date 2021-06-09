using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Web.Models.CalendarEvents
{
  public class MeetingViewModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Не заполнена тема")]
    [Display(Name = "Тема")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 120 символов")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Не заполнена дата начала")]
    [Display(Name = "Дата начала")]
    public DateTime DateStart { get; set; }

    [Required(ErrorMessage = "Не заполнена дата окончания")]
    [Display(Name = "Дата окончания")]
    public DateTime DateEnd { get; set; }

    [Required(ErrorMessage = "Не заполнено место")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 120 символов")]
    [Display(Name = "Место")]
    public string Place { get; set; }

    [Required(ErrorMessage = "Не указано значение Выполнено")]
    [Display(Name = "Выполнено")]
    public bool Done { get; set; }
  }
}
