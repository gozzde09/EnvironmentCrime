using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
  public class CoordinatorController : Controller
  {
    // Dependency Injection to get the repository
    private readonly IEnvironmentCrimeRepository repository;

    public CoordinatorController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }

    public ViewResult CrimeCoordinator(int id)
    {
      ViewBag.ID = id; // Pass the id to the view using ViewBag
      return View(repository.Departments); // Pass the department repository to the view
    }
    public ViewResult ReportCrime()
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
    public ViewResult StartCoordinator()
    {
      return View(repository);
    }
    public ViewResult Thanks()
    {
      // Retrieve the "NewErrand" object from the session
      var newErrand = HttpContext.Session.Get<Errand>("NewErrand");

      // Save the new errand to the repository
      repository.SaveErrand(newErrand);
      // Set the reference number in the ViewBag object
      ViewBag.RefNumber = newErrand.RefNumber;

      // Remove the "NewErrand" object from the session
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
