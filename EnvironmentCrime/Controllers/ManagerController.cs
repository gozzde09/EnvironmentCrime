using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class ManagerController : Controller
  {
    private readonly IEnvironmentCrimeRepository repository;
    public ManagerController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public ViewResult CrimeManager(int id)
    {
      ViewBag.ID = id;
      ViewBag.ListOfEmployees = repository.Employees;
      return View();
    }

    public ViewResult StartManager()
    {
      return View(repository);
    }

    public IActionResult SaveEmployeeOrStatus(int errandId, Employee employee)
    {

      if (employee.EmployeeId == null)
      {
        // Store an error message in TempData to display after the redirect
        TempData["Error"] = "Ingen handläggare har valts. Vänligen välj en handläggare i listan.";

        // Redirect back to the CrimeManager view with the current errandId
        return RedirectToAction("CrimeManager", new { id = errandId });
      }
      else
      {
        // Update the employee assigned to the specified errand in the repository
        repository.UpdateEmployee(errandId, employee);

        // Store a success message in TempData to display after the redirect
        TempData["Message"] = "Handläggaren har uppdaterats framgångsrikt!";

        // Redirect the user back to the StartManager view after the successful update
        return RedirectToAction("StartManager");
      }
    }
  }
}
