using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models.AppDb;

namespace EnvironmentCrime.Controllers
{
  public class CoordinatorController : Controller
  {
    // Dependency Injection to get the repository
    private readonly IEnvironmentCrimeRepository repository;

    // Constructor to inject the repository
    public CoordinatorController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public ViewResult CrimeCoordinator(int id)
    {
      ViewBag.ID = id;
      // Pass the list of departments (excluding "Småstads kommun") to the view using ViewBag
      ViewBag.ListOfDepartments = repository.Departments
      .Where(d => d.DepartmentName != "Småstads kommun")
      .ToList();

      return View();
    }
    public ViewResult ReportCrime()
    {
      // Get the "NewErrandCoord" object from the session
      var GetErrandSession = HttpContext.Session.Get<Errand>("NewErrandCoord");
      // Return the view with or without the errand object
      return GetErrandSession == null ? View() : View(GetErrandSession);
    }
    public ViewResult StartCoordinator()
    {
      return View(repository);
    }

    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      // Create object in the session
      HttpContext.Session.Set("NewErrandCoord", errand);
      return View(errand);
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
    public IActionResult SaveDepartment(int errandId, Department department)
    {
      if (department.DepartmentId == null)
      {
        // Store a error message in TempData to show after the redirect
        TempData["Error"] = "Ingen avdelning har valts. Vänligen välj en avdelning i listan.";

        // Redirect back to the CrimeCoordinator view with the errandId
        return RedirectToAction("CrimeCoordinator", new { id = errandId });
      }
      else
      {
        // Update the department for the given errand  in the repository
        repository.UpdateDepartment(errandId, department);

        // Store a success message in TempData to show after the redirect
        TempData["Message"] = "Avdelningen har uppdaterats framgångsrikt!";

        // Redirect the user back to the StartCoordinator view after the update
        return RedirectToAction("StartCoordinator");
      }
    }
  }
}
