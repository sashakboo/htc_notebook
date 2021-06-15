using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notebook.Web.Controllers
{
  public class HomeController : Controller
  {
    // GET: /Home/Error
    [HttpGet]
    public ActionResult Error()
    {
      return View();
    }
  }
}
