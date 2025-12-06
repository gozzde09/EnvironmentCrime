using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
  public class HomeController : Controller
  {
    public ViewResult Index()
    {
      // Get the "NewErrand" object from the session
      var GetErrandSession = HttpContext.Session.Get<Errand>("NewErrand");

      // Return the view with or without the errand object
      return GetErrandSession == null ? View() : View(GetErrandSession);
    }
    public ViewResult Login() => View();
  }
}
