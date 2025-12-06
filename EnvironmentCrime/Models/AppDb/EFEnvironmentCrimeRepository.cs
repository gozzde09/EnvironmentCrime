using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;

namespace EnvironmentCrime.Models.AppDb
{
  public class EFEnvironmentCrimeRepository : IEnvironmentCrimeRepository
  {
    // Private field to hold the database context
    private ApplicationDbContext context;
    private IHttpContextAccessor contextAccessor;

    // Constructor that accepts the database context via dependency injection
    public EFEnvironmentCrimeRepository(ApplicationDbContext ctx, IHttpContextAccessor contextAccessor)
    {
      context = ctx;
      this.contextAccessor = contextAccessor;
    }
    // IQueryable properties to access each entity set
    public IQueryable<Department> Departments => context.Departments;

    public IQueryable<Employee> Employees => context.Employees;

    public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

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
          if (sequence != null)
          {
            // Generate a reference number using the current year and sequence value
            newErrand.RefNumber = DateTime.Now.Year + "-45-" + sequence.CurrentValue;
            newErrand.StatusId = "S_A";
            sequence.CurrentValue++;
            context.Errands.Add(newErrand);
          }
        }
        context.SaveChanges();
      }
    }

    // Method to get errand details by ID
    public Task<Errand?> GetErrandDetails(int id)
    {
      return Task.Run(() => Errands.FirstOrDefault(e => e.ErrandId == id));
    }

    // Method to update the department of an errand - by coordinator
    public void UpdateDepartment(int errandId, Department department)
    {
      // Find the errand by its ID
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);

      if (errandDb != null)
      {
        // Update the DepartmentId of the errand
        errandDb.DepartmentId = department.DepartmentId;
      }
      context.SaveChanges();
    }
    // Method to update the employee of an errand - by manager
    public void UpdateEmployee(int errandId, Employee employee)
    {
      // Find the errand by its ID
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);

      if (errandDb != null)
      {
        // Update the EmployeeId of the errand
        errandDb.EmployeeId = employee.EmployeeId;
        errandDb.StatusId = "S_A";
      }
      context.SaveChanges();
    }

    // Method to update investigator info of an errand - by manager
    public void UpdateInvestigatorInfo(int errandId, string investigatorInfo)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Update the InvestigatorInfo of the errand
        errandDb.InvestigatorInfo = errandDb.InvestigatorInfo + "\n" + investigatorInfo;
        errandDb.StatusId = "S_B";
        errandDb.EmployeeId = "";
      }
      context.SaveChanges();
    }
    // Method to add investigator info for an errand - by investigator
    public void AddInvestigatorInfo(int errandId, string investigatorInfo)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Set the InvestigatorInfo of the errandDb
        errandDb.InvestigatorInfo = errandDb.InvestigatorInfo + "\n" + investigatorInfo;
      }
      context.SaveChanges();
    }

    // Metod to change status for an errand - by investigator
    public void UpdateErrandStatus(int errandId, string statusId)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Update the StatusId of the errand
        errandDb.StatusId = statusId;
      }
      context.SaveChanges();
    }

    // Method to create an investigator event for an errand - by investigator
    public void AddInvestigatorEvent(int errandId, string investigatorAction)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Update the InvestigatorAction of the errand
        errandDb.InvestigatorAction = errandDb.InvestigatorAction + "\n" + investigatorAction;
      }
      context.SaveChanges();
    }
    // Method to insert a file (sample or picture) associated with an errand - by investigator
    public async Task InsertFileAsync(string dirPath, int errandId, string uniqueFileName)
    {
      if (dirPath == "Samples")
      {
        var sample = new Sample
        {
          ErrandId = errandId,
          SampleName = uniqueFileName
        };
        await context.Samples.AddAsync(sample);
      }
      if (dirPath == "Pictures")
      {
        var picture = new Picture
        {
          ErrandId = errandId,
          PictureName = uniqueFileName
        };
        await context.Pictures.AddAsync(picture);
      }
      await context.SaveChangesAsync();
    }
    // Method to get employee details by username
    public Employee GetEmployee(string userName)
    {
      Employee emp = new Employee();
      foreach (Employee em in Employees)
      {
        if (em.EmployeeId == userName)
        {
          emp = em;
        }
      }
      return emp;

    }
    // Method to get errands based on user role
    public IQueryable<Case> GetErrands(string role)
    {
      var userName = contextAccessor.HttpContext?.User?.Identity?.Name;

      if (string.IsNullOrWhiteSpace(userName))
        throw new InvalidOperationException("Ingen inloggad användare hittades.");

      var employee = GetEmployee(userName);

      if (employee == null)
        throw new InvalidOperationException("Användaren hittades inte i Employees-tabellen.");

      var query =
          // Join Errands with related tables to get necessary details
          from errand in Errands
          join status in ErrandStatuses on errand.StatusId equals status.StatusId

          join department in Departments on errand.DepartmentId equals department.DepartmentId into deptJoin
          from dep in deptJoin.DefaultIfEmpty()

          join employeeJoin in Employees on errand.EmployeeId equals employeeJoin.EmployeeId into empJoin
          from emp in empJoin.DefaultIfEmpty()

            // Project the results into an anonymous object
          select new
          {
            Errand = errand,
            StatusName = status.StatusName,
            DepartmentName = dep != null ? dep.DepartmentName : "Ej tillsatt",
            EmployeeName = emp != null ? emp.EmployeeName : "Ej tillsatt",
            // Include DepartmentId and EmployeeId for filtering
            DepartmentId = dep.DepartmentId,
            EmployeeId = emp.EmployeeId
          };
      switch (role.ToLower())
      {
        case "coordinator":
          // Sees all the cases
          break;

        case "manager":
          // Sees cases assigned to their department
          query = query.Where(x => x.DepartmentId == employee.DepartmentId);
          break;

        case "investigator":
          // Sees cases assigned to them
          query = query.Where(x => x.EmployeeId == employee.EmployeeId);
          break;

        default:
          throw new ArgumentException("Ogiltig roll.");
      }
      var result =
          query.OrderByDescending(x => x.Errand.RefNumber)
               .Select(x => new Case
               {
                 ErrandId = x.Errand.ErrandId,
                 DateOfObservation = x.Errand.DateOfObservation,
                 RefNumber = x.Errand.RefNumber,
                 TypeOfCrime = x.Errand.TypeOfCrime,
                 StatusName = x.StatusName,
                 DepartmentName = x.DepartmentName,
                 EmployeeName = x.EmployeeName
               });

      return result;
    }

    // Gets manager's employees by their association depId. 
    public IQueryable<Case> GetManagerEmployeeList()
    {
      var userName = contextAccessor.HttpContext?.User?.Identity?.Name;

      var errandList = from emp0 in Employees
                       join dep in Departments on emp0.DepartmentId equals dep.DepartmentId
                       join emp1 in Employees on dep.DepartmentId equals emp1.DepartmentId
                       where emp0.EmployeeId == userName

                       select new Case
                       {
                         EmployeeName = (emp1.EmployeeName)
                       };

      return errandList;
    }
  }
}
