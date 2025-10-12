using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CoordinatorController : Controller
  {
    private readonly IEnvironmentCrimeRepository repository;
    public CoordinatorController(IEnvironmentCrimeRepository repo)
    {
     repository = repo;
    }
    public ViewResult CrimeCoordinator(string id)
    {
      ViewBag.ID = id;
      return View(repository);
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
    public ViewResult Validate()
    {
      return View();
    }

  }
}
