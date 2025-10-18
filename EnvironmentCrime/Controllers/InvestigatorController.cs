using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class InvestigatorController : Controller
  {
    private readonly IEnvironmentCrimeRepository repository;
    public InvestigatorController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public ViewResult CrimeInvestigator(string id)
    {
      ViewBag.ID = id;
      return View(repository.ErrandStatuses);
    }

    public ViewResult StartInvestigator()
    {
      return View(repository);
    }
  }
}
