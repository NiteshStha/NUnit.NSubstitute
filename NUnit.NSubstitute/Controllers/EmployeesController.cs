using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) 
                return NotFound();
            return Ok(employee);
        }
    }
}
