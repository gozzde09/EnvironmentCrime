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
    public ViewResult CrimeManager(string id)
    {
      ViewBag.ID = id;
      return View(repository);
    }

    public ViewResult StartManager()
    {
      return View(repository);
    }
  }
}
