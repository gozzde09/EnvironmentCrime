using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
  public class HomeController : Controller
  {
    public ViewResult Index()
    {
      var GetErrandSession = HttpContext.Session.Get<Errand>("NewErrand");
      if (GetErrandSession == null)
      {
        return View();
      }
      else
      {
        return View(GetErrandSession);
      }
    }
    public ViewResult Login() => View();

  }
}
