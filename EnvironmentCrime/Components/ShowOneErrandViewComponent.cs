using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Components
{
  public class ShowOneErrandViewComponent : ViewComponent
  {
    private IEnvironmentCrimeRepository repository;
    public ShowOneErrandViewComponent(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync(string id)
    {
      var ErrandDetails = await repository.GetErrandDetails(id);
      return View(ErrandDetails);
    }
  }
}
