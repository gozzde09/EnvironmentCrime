using EnvironmentCrime.Models.POCO;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{
  public class EFEnvironmentCrimeRepository : IEnvironmentCrimeRepository
  {
    private ApplicationDbContext context;

    // Constructor that accepts the database context via dependency injection
    public EFEnvironmentCrimeRepository(ApplicationDbContext ctx)
    {
      context = ctx;
    }
    public IQueryable<Department> Departments => context.Departments;

    public IQueryable<Employee> Employees => context.Employees;

    public IQueryable<Errand> Errands => context.Errands;

    public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;

    public IQueryable<Picture> Pictures => context.Pictures;

    public IQueryable<Sample> Samples => context.Samples;

    public IQueryable<Sequence> Sequences => context.Sequences;

    // Method to save a new errand to the database
    public void SaveErrand(Errand newErrand)
    {
      {
        if (newErrand.ErrandId == 0)
        {
          var sequence = Sequences.FirstOrDefault(s => s.Id == 1);
          newErrand.RefNumber = "2022-45-" + sequence.CurrentValue;
          newErrand.StatusId = "S_A";
          sequence.CurrentValue++;
          context.Errands.Add(newErrand);
          
        }
        context.SaveChanges();
      }
    }

    // Method to get errand details by ID
    public Task<Errand?> GetErrandDetails(int id)
    {
      return Task.Run(() =>
      {
        var errandDetail = Errands.Where(er => er.ErrandId == id).FirstOrDefault();
        return errandDetail;
      });
    }

  }
}

