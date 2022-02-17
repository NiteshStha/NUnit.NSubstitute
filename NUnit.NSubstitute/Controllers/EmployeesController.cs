using Microsoft.AspNetCore.Mvc;
using NUnit.NSubstitute.Models;
using NUnit.NSubstitute.Services;

namespace NUnit.NSubstitute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _service.Get();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            _service.Create(employee);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            var updatedEmployee = _service.Update(id, employee);
            if (updatedEmployee == null)
                return NotFound();
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedEmployee = _service.Delete(id);
            if (deletedEmployee == null)
                return NotFound();
            return Ok(deletedEmployee);
        }
    }
}
