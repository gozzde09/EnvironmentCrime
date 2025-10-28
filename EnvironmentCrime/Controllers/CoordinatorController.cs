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
      var GetErrandSession = HttpContext.Session.Get<Errand>("NewErrandCoord");

      return GetErrandSession == null ? View() : View(GetErrandSession);
    }
    public ViewResult StartCoordinator()
    {
      return View(repository);
    }
    public ViewResult Thanks()
    {
      // Retrieve the "NewErrandCoord" object from the session
      var newErrand = HttpContext.Session.Get<Errand>("NewErrandCoord");
      if (newErrand is null)
      {
        ViewBag.RefNumber = "Fel med sessionen, registrera ärendet igen!";
      }
      else
      { 
        // Save the new errand to the repository
        repository.SaveErrand(newErrand);
        // Set the reference number in the ViewBag object
        ViewBag.RefNumber = newErrand.RefNumber;
      }

      // Remove the "NewErrandCoord" object from the session
      HttpContext.Session.Remove("NewErrandCoord");
      return View();
    }

    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      HttpContext.Session.Set("NewErrandCoord", errand);
      return View(errand);
    }

    [HttpPost]
    public IActionResult SaveDepartment(int errandId, string choosenDepartment)
    {
      if (choosenDepartment != "Välj")
      {
        repository.UpdateDepartment(errandId, choosenDepartment);
        TempData["Message"] = "Avdelningen har uppdaterats framgångsrikt!";
        return RedirectToAction("CrimeCoordinator", new { id = errandId });
      }
     
      // Add a return value for the case when choosenDepartment == "Välj"
      TempData["Error"] = "Vänligen välj en ny avdelning från menyn.";

      // Redirect back to the CrimeCoordinator view with the errandId
      return RedirectToAction("CrimeCoordinator", new { id = errandId });
    }
  }
}
