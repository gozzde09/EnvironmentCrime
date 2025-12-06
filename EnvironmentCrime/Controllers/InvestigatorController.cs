using EnvironmentCrime.Models.AppDb;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class InvestigatorController : Controller
  {
    // Dependency Injection to get the repository
    private readonly IEnvironmentCrimeRepository repository;

    // Dependency Injection to get the web host environment
    private readonly IWebHostEnvironment environment;

    // Constructor to inject the repository and environment
    public InvestigatorController(IEnvironmentCrimeRepository repo, IWebHostEnvironment env)
    {
      repository = repo;
      environment = env;
    }
    public ViewResult CrimeInvestigator(int id)
    {
      // Only "påbörjad" and "klar"
      ViewBag.ListOfStatus = repository.ErrandStatuses.
        Where(e => e.StatusId == "S_C" || e.StatusId == "S_D").ToList();

      // Pass the errandId to the view using ViewBag
      ViewBag.ID = id;
      return View();
    }
    public ViewResult StartInvestigator()
    {
      return View(repository);
    }

    // Save pictures and samples
    private async Task SaveFileAsync(int errandId, string dirPath, IFormFile? documents)
    {
      // Check if a file was uploaded
      if (documents == null || documents.Length == 0)
        return;

      // Uniq file name
      string uniqueFileName = Guid.NewGuid().ToString() + "_" + documents.FileName;

      // Path to save file
      string fullPath = Path.Combine(environment.WebRootPath, dirPath, uniqueFileName);

      // Save the uploaded file
      await using var stream = new FileStream(fullPath, FileMode.Create);
      await documents.CopyToAsync(stream);

      // Save to database
      await repository.InsertFileAsync(dirPath, errandId, uniqueFileName);

    }

    [HttpPost]
    public async Task<IActionResult> UpdateErrandAsInvestigator(int errandId, string investigatorInfo, string investigatorAction, string statusId, IFormFile sample, IFormFile picture)
    {
      // Check if at least one field is filled
      if (string.IsNullOrWhiteSpace(investigatorInfo) &&
          string.IsNullOrWhiteSpace(investigatorAction) &&
          string.IsNullOrWhiteSpace(statusId) &&
          sample == null &&
          picture == null)
      {
        TempData["Error"] = "Ingen ändring gjordes. Inget fält har fyllts i.";
        return RedirectToAction("CrimeInvestigator", new { id = errandId });
      }
      // Add investigator info, action and status if provided
      if (!string.IsNullOrWhiteSpace(investigatorInfo))
        repository.AddInvestigatorInfo(errandId, investigatorInfo);

      if (!string.IsNullOrWhiteSpace(investigatorAction))
        repository.AddInvestigatorEvent(errandId, investigatorAction);

      if (!string.IsNullOrWhiteSpace(statusId))
      { repository.UpdateErrandStatus(errandId, statusId); }

      // Save sample and picture if provided
      await SaveFileAsync(errandId, "Samples", sample);

      await SaveFileAsync(errandId, "Pictures", picture);

      TempData["Message"] = "Ärendet har uppdaterats framgångsrikt!";
      return RedirectToAction("CrimeInvestigator", new { id = errandId });
    }
  }
}
