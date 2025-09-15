using MrLeastAcademy.Models;

namespace MrLeastAcademy.Repository
{
    public interface IEmployeeRepository
    {
        public void Add(Employee employee);


        // Update
        public void Update(Employee employee);


        // Delete
        public void Delete(int id);


        // Read All
        public List<Employee> GetAll();

        // Read By Id
        public Employee GetById(int id);

        public void Save();
       
        public List<Employee> GetEmployeesByDepartment(int departmentId);
    }
}
