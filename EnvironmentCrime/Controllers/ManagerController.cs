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

    public IActionResult SaveEmployeeOrStatus(int errandId, Employee employee, bool noAction,string InvestigatorInfo)
    {
      if (!noAction && employee.EmployeeId == null)
      {
        // Store an error message in TempData to display after the redirect
        TempData["Error"] = "Ingen ändring gjordes. Välj en handläggare eller markera 'Ingen åtgärd' med kommentar.";
        return RedirectToAction("CrimeManager", new { id = errandId });
      }
      if (noAction)
      {
        if(InvestigatorInfo == null || InvestigatorInfo.Trim().Length == 0)
        {
          // Store an error message in TempData to display after the redirect
          TempData["Error"] = "Ingen ändring gjordes. Kommentaren för 'Ingen åtgärd' får inte vara tom.";
          return RedirectToAction("CrimeManager", new { id = errandId });
        }
        repository.UpdateInvestigatorInfo(errandId, InvestigatorInfo);

        TempData["Message"] = "Ärendet har markerats som 'Ingen åtgärd' och kommentaren har sparats.";
        
        // Redirect back to the StartManager view 
        return RedirectToAction("StartManager");
      }
     if(employee.EmployeeId !=null)
      {
        // Update the employee assigned to the specified errand in the repository
        repository.UpdateEmployee(errandId, employee);
        
        // Store a success message in TempData to display after the redirect
        TempData["Message"] = "Handläggare har lagts till i ärendet framgångsrikt!";

        // Redirect the user back to the StartManager view after the successful update
        return RedirectToAction("StartManager");
      }
      TempData["Error"] = "Okänt fel — inga ändringar sparades.";
      return RedirectToAction("CrimeManager", new { id = errandId });
    }
  }
}
