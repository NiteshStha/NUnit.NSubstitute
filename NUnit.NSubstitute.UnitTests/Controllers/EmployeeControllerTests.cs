using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.NSubstitute.Controllers;
using NUnit.NSubstitute.Models;
using NUnit.NSubstitute.Services;
using System.Collections.Generic;

namespace NUnit.NSubstitute.UnitTests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private IEmployeeService _service;
        private EmployeesController _controller;
        private Employee _employee;

        [SetUp]
        public void SetUp()
        {
            _service = Substitute.For<IEmployeeService>();
            _controller = new EmployeesController(_service);
            _employee = new Employee { Id = 1, Name = "John", Address = "Kathmandu" };
        }

        [Test]
        public void Get_WhenCalled_ReturnsListOfEmployees()
        {
            // Arrange
            _service.Get()
                .Returns(new List<Employee>
                {
                    _employee
                });

            // Act
            var result = _controller.Get();
            var okObject = result as OkObjectResult;

            // Assert
            _service.Received(1).Get();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.IsNotNull(okObject);
            Assert.AreEqual(okObject?.StatusCode, 200);
        }

        [Test]
        public void GetById_EmployeeIdDoesNotExists_ReturnsNotFoundObjectResult()
        {
            // Arrange
            _service.GetById(1).ReturnsNull();

            // Act
            var result = _controller.GetById(1);
            var notFoundObject = result as NotFoundResult;

            // Assert
            _service.Received(1).GetById(1);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual(notFoundObject?.StatusCode, 404);
        }

        [Test]
        public void GetById_EmployeeIdExists_ReturnsOkObjectResult()
        {
            _service.GetById(1)
                .Returns(_employee);

            var result = _controller.GetById(1);
            var okObject = result as OkObjectResult;
            var objectValue = okObject?.Value as Employee;

            _service.Received(1).GetById(1);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(okObject?.Value);
            Assert.IsInstanceOf<Employee>(okObject?.Value);
            Assert.AreEqual(okObject?.StatusCode, 200);
            Assert.AreEqual(objectValue?.Id, _employee.Id);
            Assert.AreEqual(objectValue?.Name, _employee.Name);
            Assert.AreEqual(objectValue?.Address, _employee.Address);
        }

        [Test]
        public void Post_WhenCalledWithEmployeeObject_ReturnsOkObject()
        {
            var result = _controller.Post(_employee);

            _service.Received(1).Create(_employee);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Update_EmployeeIdDoesNotExists_ReturnNotFoundResult()
        {
            _service.Update(100, _employee).ReturnsNull();

            var result = _controller.Put(100, _employee);

            _service.Received(1).Update(100, _employee);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Update_EmployeeIdExists_ReturnOkObjectResult()
        {
            _service.Update(1, _employee).Returns(_employee);

            var result = _controller.Put(1, _employee);
            var okObject = result as OkObjectResult;
            var objectValue = okObject?.Value as Employee;

            _service.Received(1).Update(1, _employee);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(objectValue, _employee);
        }

        [Test]
        public void Delete_EmployeeIdDoesNotExists_ReturnNotFoundResult()
        {
            _service.Delete(100).ReturnsNull();

            var result = _controller.Delete(100);

            _service.Received(1).Delete(100);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_EmployeeIdExists_ReturnOkObjectResult()
        {
            _service.Delete(1).Returns(_employee);

            var result = _controller.Delete(1);
            var okObject = result as OkObjectResult;
            var objectValue = okObject?.Value as Employee;

            _service.Received(1).Delete(1);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(objectValue, _employee);
        }
    }
}
