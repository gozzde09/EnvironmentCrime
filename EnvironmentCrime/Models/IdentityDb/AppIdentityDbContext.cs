using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models.IdentityDb
{
  public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
  {  // Constructor
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
  }
}
