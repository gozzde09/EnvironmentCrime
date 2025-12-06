using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using EnvironmentCrime.Models.AppDb;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CitizenController : Controller
  {
    // Dependency Injection to get the repository
    private readonly IEnvironmentCrimeRepository repository;

    // Constructor to inject the repository
    public CitizenController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public ViewResult Contact() => View();
    public ViewResult Faq() => View();
    public ViewResult Services() => View();

    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      // Create object in the session 
      HttpContext.Session.Set("NewErrand", errand);
      return View(errand);
    }
    public ViewResult Thanks()
    {
      // Retrieve the "NewErrand" object from the session
      var newErrand = HttpContext.Session.Get<Errand>("NewErrand");

      if(newErrand is null)
      {
        ViewBag.RefNumber = "Fel med sessionen, registrera ärendet igen!";
        return View();
      }

      // Save the new errand to the repository
      repository.SaveErrand(newErrand);
      
      // Set the reference number in the ViewBag object
      ViewBag.RefNumber = newErrand.RefNumber;

      // Remove the "NewErrand" object from the session
      HttpContext.Session.Remove("NewErrand");
      return View();
    }
  }
}
