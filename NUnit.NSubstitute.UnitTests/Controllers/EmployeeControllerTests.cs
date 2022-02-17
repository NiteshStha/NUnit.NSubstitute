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

        [SetUp]
        public void SetUp()
        {
            _service = Substitute.For<IEmployeeService>();
            _controller = new EmployeesController(_service);
        }

        [Test]
        public void Get_WhenCalled_ReturnsListOfEmployees()
        {
            // Arrange
            _service.Get()
                .Returns(new List<Employee> 
                {
                    new Employee { Id = 1, Name = "John Doe", Address = "America" },
                    new Employee { Id = 2, Name = "Chris Jericho", Address = "Canada" },
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
            _service.Received().GetById(1);
            Assert.IsInstanceOf<NotFoundResult>(result);
            Assert.AreEqual(notFoundObject?.StatusCode, 404);
        }

        [Test]
        public void GetById_EmployeeIdExists_ReturnsOkObjectResult()
        {
            _service.GetById(1)
                .Returns(new Employee { Id = 1, Name = "John Doe", Address = "America" });

            var result = _controller.GetById(1);
            var okObject = result as OkObjectResult;
            var employee = okObject?.Value as Employee;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(okObject?.Value);
            Assert.IsInstanceOf<Employee>(okObject?.Value);
            Assert.AreEqual(okObject?.StatusCode, 200);
            Assert.AreEqual(employee?.Id, 1);
            Assert.AreEqual(employee?.Name, "John Doe");
            Assert.AreEqual(employee?.Address, "America");
        }
    }
}
