using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Notebook.Web.Models.CalendarEvents
{
  public class CalendarEventIndexItem
  {
    public int Id { get; set; }

    [Display(Name = "Тип")]
    public CalendarEventType EventType { get; set; }

    [Display(Name = "Тема")]
    public string Subject { get; set; }

    [Display(Name = "Дата начала")]
    public DateTime DateStart { get; set; }

    [Display(Name = "Дата окончания")]
    public DateTime? DateEnd { get; set; }

    [Display(Name = "Место")]
    public string Place { get; set; }

    [Display(Name = "Выполнено")]
    public bool Done { get; set; }
  }
}
