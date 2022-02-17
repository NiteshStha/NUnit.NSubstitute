using NUnit.NSubstitute.Models;

namespace NUnit.NSubstitute.Services
{
    public interface IEmployeeService
    {
        List<Employee> Get();

        Employee? GetById(int id);

        void Create(Employee employee);

        Employee? Update(int id, Employee employee);

        Employee? Delete(int id);
    }
}