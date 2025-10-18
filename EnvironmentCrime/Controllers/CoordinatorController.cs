using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    public ViewResult CrimeCoordinator(string id)
    {
      ViewBag.ID = id; // Pass the id to the view using ViewBag
      return View(repository.Departments); // Pass the department repository to the view
    }
    public ViewResult ReportCrime()
    {
      return View();
    }
    public ViewResult StartCoordinator()
    {
      return View(repository);
    }
    public ViewResult Thanks()
    {
      return View();
    }
    public ViewResult Validate(Errand errand)
    {
      // TODO? HttpContext.Session 
      return View(errand);
    }

  }
}
