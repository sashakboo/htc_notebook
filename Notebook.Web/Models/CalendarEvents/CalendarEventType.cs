using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Web.Models.CalendarEvents
{
  public enum CalendarEventType
  {
    [Display(Name = "Все")]
    All = 0,
    [Display(Name = "Встреча")]
    Meeting = 1,
    [Display(Name = "Памятка")]
    Memo = 2,
    [Display(Name = "Дело")]
    Work = 3
  }
}
