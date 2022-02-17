using NUnit.NSubstitute.Models;

namespace NUnit.NSubstitute.Services
{
    public interface IEmployeeService
    {
        List<Employee> Get();
        Employee? GetById(int id);
    }
}