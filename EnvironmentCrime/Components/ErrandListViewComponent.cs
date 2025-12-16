using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models.AppDb;

namespace EnvironmentCrime.Components
{
  public class ErrandListViewComponent : ViewComponent
  {
    private readonly IEnvironmentCrimeRepository _repo;

    public ErrandListViewComponent(IEnvironmentCrimeRepository repo)
    {
      _repo = repo;
    }

    public IViewComponentResult Invoke(
        string role,
        string? statusFilter,
        string? departmentFilter,
        string? employeeFilter,
        string? caseNumberFilter)
    {
      var errands = _repo.FilteredErrands(
          role,
          statusFilter,
          departmentFilter,
          employeeFilter,
          caseNumberFilter
      );

      return View(errands);
    }
  }
}
