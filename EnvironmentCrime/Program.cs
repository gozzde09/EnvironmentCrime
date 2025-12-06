using EnvironmentCrime.Models.AppDb;
using EnvironmentCrime.Models.IdentityDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the repository for dependency injection
builder.Services.AddTransient<IEnvironmentCrimeRepository,EFEnvironmentCrimeRepository>();

// Configure the database context to use SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity services
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

// Set up Identity with Entity Framework stores
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddSession();

var app = builder.Build();

// Ensure the database is populated with initial data
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  DBInitializer.EnsurePopulated(services);
  IdentityInitializer.EnsurePopulated(services).Wait();
}

// Configure the HTTP request pipeline.
  if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

// Enable session middleware
app.UseSession();

// Enable authentication and authorization middleware
app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
