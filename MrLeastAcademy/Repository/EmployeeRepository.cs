using MrLeastAcademy.Models;

namespace MrLeastAcademy.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        AppDbContext context;
        public EmployeeRepository(AppDbContext _context)
        {
            context = _context;
        }
        // CRUD

        // Create
        public void Add(Employee employee)
        {
            context.Add(employee);

        }

        // Update
        public void Update(Employee employee)
        {
            context.Update(employee);
        }

        // Delete
        public void Delete(int id)
        {
            Employee employee = GetById(id);
            context.Remove(employee);
        }

        // Read All
        public List<Employee> GetAll()
        {
            return context.Employees.ToList();

        }
        // Read By Id
        public Employee GetById(int id)
        {
            return context.Employees.FirstOrDefault(x => x.Id == id);
        }
        // Save Changes
        public void Save()
        {
            context.SaveChanges();
        }

        public List<Employee> GetEmployeesByDepartment(int departmentId)
        {
            return context.Employees.Where(x => x.DepartmentId == departmentId).ToList();
        }
    }
}
