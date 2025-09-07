using MrLeastAcademy.Models;

namespace MrLeastAcademy.Repository
{
    public interface IDepartmentRepository
    {
        // Create
        public void Add(Department department);


        // Update
        public void Update(Department department);


        // Delete
        public void Delete(int id);


        // Read All
        public List<Department> GetAll();

        // Read By Id
        public Department GetById(int id);

        // Save Changes
        public void Save();

        public bool IsValidDepartmentName(string name, int currentDeptId = 0);


        public bool Exists(int id);
        public Department GetByIdWithEmployees(int id);
    }
}
