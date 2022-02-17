using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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
    }
}
