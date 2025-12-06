using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models.AppDb;

namespace EnvironmentCrime.Components
{
  public class ShowOneErrandViewComponent : ViewComponent
  {
    private IEnvironmentCrimeRepository repository;

    // Constructor to inject the repository
    public ShowOneErrandViewComponent(IEnvironmentCrimeRepository repo)
    {
      repository = repo;
    }
    // The InvokeAsync method is called when the view component is invoked in a Razor view.
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
      // Fetch the errand details using the repository
      var ErrandDetails = await repository.GetErrandDetails(id);
      return View(ErrandDetails);
    }
  }
}
