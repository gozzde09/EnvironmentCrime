using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CitizenController : Controller
  {
    private readonly IEnvironmentCrimeRepository repository;

    public CitizenController(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public ViewResult Contact() => View();
    public ViewResult Faq() => View();
    public ViewResult Services() => View();

    [HttpPost]
    public ViewResult Validate(Errand errand)
    {
      return View(errand);
    }
    public ViewResult Thanks() => View();

  }
}
