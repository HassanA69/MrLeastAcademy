using Microsoft.EntityFrameworkCore;
using MrLeastAcademy.Models;

namespace MrLeastAcademy.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        AppDbContext context;
        public DepartmentRepository(AppDbContext _context)
        {
            context = _context;
        }
        // CRUD

        // Create
        public void Add(Department department)
        {
            context.Add(department);

        }

        // Update
        public void Update(Department department)
        {
            context.Update(department);
        }

        // Delete
        public void Delete(int id)
        {
            Department department = GetById(id);
            context.Remove(department);
        }


        // Read All
        public List<Department> GetAll()
        {
            return context.Departments.ToList();

        }
        // Read By Id
        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(x => x.Id == id);
        }
        // Save Changes
        public void Save()
        {
            context.SaveChanges();
        }
        public bool IsValidDepartmentName(string name, int currentDeptId = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var dept = context.Departments
                .FirstOrDefault(x => x.Name.ToLower() == name.ToLower() && x.Id != currentDeptId);

            return dept == null;
        }

        public bool Exists(int id)
        {
            return context.Departments.Any(e => e.Id == id);
        }

        public Department GetByIdWithEmployees(int id)
        {
            return context.Departments
                .Include(d => d.Employees)
                .FirstOrDefault(d => d.Id == id);
        }

    }
}
