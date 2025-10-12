using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Controllers
{
  public class HomeController : Controller
  {

    public ViewResult Index() => View(); 
    public ViewResult Login() => View();

  }
}
