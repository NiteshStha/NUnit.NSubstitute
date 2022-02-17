using NUnit.NSubstitute.Models;

namespace NUnit.NSubstitute.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeService()
        {
            _employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Address = "America" },
                new Employee { Id = 2, Name = "Chris Jericho", Address = "Canada" },
                new Employee { Id = 2, Name = "Son Hueng Min", Address = "South Korea" },
            };
        }

        public List<Employee> Get() => _employees;

        public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public void Create(Employee employee) => _employees.Add(employee);

        public Employee? Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return null;

            var employeeToUpdate = GetById(id);
            if (employeeToUpdate == null)
                return null;

            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Address = employee.Address;

            return employeeToUpdate;
        }

        public Employee? Delete(int id)
        {
            var employeeToDelete = GetById(id);
            if (employeeToDelete == null)
                return null;

            _employees.Remove(employeeToDelete);
            return employeeToDelete;
        }
    }
}
