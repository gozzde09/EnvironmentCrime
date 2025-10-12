using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class CitizenController : Controller
  {
    public ViewResult Contact() => View();
    public ViewResult Faq() => View();
    public ViewResult Services() => View();
    public ViewResult Validate() => View();
    public ViewResult Thanks() => View();

  }
}
