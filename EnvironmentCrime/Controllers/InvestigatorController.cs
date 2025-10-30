using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;

namespace EnvironmentCrime.Controllers
{
  public class InvestigatorController : Controller
  {
    private readonly IEnvironmentCrimeRepository repository;
    private readonly IWebHostEnvironment environment;
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
      if (string.IsNullOrWhiteSpace(investigatorInfo) &&
          string.IsNullOrWhiteSpace(investigatorAction) &&
          string.IsNullOrWhiteSpace(statusId) &&
          sample == null &&
          picture == null)
      {
        TempData["Error"] = "Ingen ändring gjordes. Inget fält har fyllts i.";
        return RedirectToAction("CrimeInvestigator", new { id = errandId });
      }

      if (!string.IsNullOrWhiteSpace(investigatorInfo))
        repository.AddInvestigatorInfo(errandId, investigatorInfo);

      if (!string.IsNullOrWhiteSpace(investigatorAction))
        repository.AddInvestigatorEvent(errandId, investigatorAction);

      if (!string.IsNullOrWhiteSpace(statusId))
      { repository.UpdateErrandStatus(errandId, statusId); }

      await SaveFileAsync(errandId, "Samples", sample);

      await SaveFileAsync(errandId, "Pictures", picture);

      TempData["Message"] = "Ärendet har uppdaterats framgångsrikt!";
      return RedirectToAction("CrimeInvestigator", new { id = errandId });
    }
  }
}
