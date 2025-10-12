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
    public ViewResult CrimeInvestigator()
    {
      return View();
    }

    public ViewResult StartInvestigator()
    {
      return View(repository);
    }
  }
}
