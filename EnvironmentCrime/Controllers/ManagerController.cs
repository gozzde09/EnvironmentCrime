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
      return View(repository.Employees);
    }

    public ViewResult StartManager()
    {
      return View(repository);
    }
  }
}
