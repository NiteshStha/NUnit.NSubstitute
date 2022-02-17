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
    }
}
