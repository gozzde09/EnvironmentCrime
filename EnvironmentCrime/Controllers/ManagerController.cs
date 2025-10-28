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

      if (employee.EmployeeId == "Välj")
      {
        // Add a return value for the case when employee.EmployeeName == "Välj"
        TempData["Error"] = "Vänligen välj en handläggare från menyn."; 
      }
      else
      {
        repository.UpdateEmployee(errandId, employee);
        TempData["Message"] = "Handläggaren har uppdaterats framgångsrikt!";
      }
      // Redirect back to the CrimeCoordinator view with the errandId
      return RedirectToAction("CrimeManager", new { id = errandId });
    }
  }
}
