using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CitizenController : Controller
  {
    private readonly IEnvironmentCrimeRepository repository;

    public CitizenController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public ViewResult Contact() => View();
    public ViewResult Faq() => View();
    public ViewResult Services() => View();

    public ViewResult Thanks()
    {
      HttpContext.Session.Remove("NewErrand");
      return View();
    }

    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      HttpContext.Session.Set("NewErrand", errand);
      return View(errand);
    }
  }
}
