using HealthSysHub.Web.API.Controllers;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HealthSysHub.Web.Tests.Controllers
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        private Mock<IDepartmentManager> _mockDepartmentManager;
        private DepartmentController _controller;

        [SetUp]
        public void Setup()
        {
            _mockDepartmentManager = new Mock<IDepartmentManager>();
            _controller = new DepartmentController(_mockDepartmentManager.Object);
        }

        #region GetDepartmentsAsync Tests

        [Test]
        public async Task GetDepartmentsAsync_ReturnsOkResultWithDepartments()
        {
            // Arrange
            var expectedDepartments = new List<Department>
            {
                new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Cardiology" },
                new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurology" }
            };

            _mockDepartmentManager.Setup(x => x.GetDepartmentsAsync())
                .ReturnsAsync(expectedDepartments);

            // Act
            var result = await _controller.GetDepartmentsAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedDepartments, okResult.Value);
        }

        [Test]
        public async Task GetDepartmentsAsync_Returns500WhenExceptionOccurs()
        {
            // Arrange
            var exceptionMessage = "Database connection failed";
            _mockDepartmentManager.Setup(x => x.GetDepartmentsAsync())
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetDepartmentsAsync();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual(exceptionMessage, objectResult.Value);
        }

        #endregion

        #region GetDepartmentByIdAsync Tests

        [Test]
        public async Task GetDepartmentByIdAsync_ReturnsOkResultWithDepartment()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var expectedDepartment = new Department { DepartmentId = departmentId, DepartmentName = "Cardiology" };

            _mockDepartmentManager.Setup(x => x.GetDepartmentByIdAsync(departmentId))
                .ReturnsAsync(expectedDepartment);

            // Act
            var result = await _controller.GetDepartmentByIdAsync(departmentId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedDepartment, okResult.Value);
        }

        [Test]
        public async Task GetDepartmentByIdAsync_Returns500WhenExceptionOccurs()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var exceptionMessage = "Invalid department ID";
            _mockDepartmentManager.Setup(x => x.GetDepartmentByIdAsync(departmentId))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetDepartmentByIdAsync(departmentId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual(exceptionMessage, objectResult.Value);
        }

        [Test]
        public async Task GetDepartmentByIdAsync_Returns500WhenGuidIsEmpty()
        {
            // Arrange
            var emptyGuid = Guid.Empty;
            _mockDepartmentManager.Setup(x => x.GetDepartmentByIdAsync(emptyGuid))
                .ThrowsAsync(new ArgumentException("Department ID cannot be empty"));

            // Act
            var result = await _controller.GetDepartmentByIdAsync(emptyGuid);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        #endregion

        #region InsertOrUpdateDepartment Tests

        [Test]
        public async Task InsertOrUpdateDepartment_ReturnsOkResultWithSuccessResponse()
        {
            // Arrange
            var department = new Department {DepartmentId = Guid.NewGuid(), DepartmentName = "Cardiology" };
            //var expectedResponse = new OperationResponse { Success = true, Message = "Department saved successfully" };

            _mockDepartmentManager.Setup(x => x.InsertOrUpdateDepartmentAsync(department))
                .ReturnsAsync(department);

            // Act
            var result = await _controller.InsertOrUpdateDepartment(department);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(department, okResult.Value);
        }

        [Test]
        public async Task InsertOrUpdateDepartment_Returns500WhenExceptionOccurs()
        {
            // Arrange
            var department = new Department {DepartmentId = Guid.NewGuid(), DepartmentName = "Cardiology" };
            var exceptionMessage = "Validation failed";
            _mockDepartmentManager.Setup(x => x.InsertOrUpdateDepartmentAsync(department))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.InsertOrUpdateDepartment(department);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual(exceptionMessage, objectResult.Value);
        }

        [Test]
        public async Task InsertOrUpdateDepartment_Returns500WhenDepartmentIsNull()
        {
            // Arrange
            Department department = null;
            _mockDepartmentManager.Setup(x => x.InsertOrUpdateDepartmentAsync(department))
                .ThrowsAsync(new ArgumentNullException(nameof(department)));

            // Act
            var result = await _controller.InsertOrUpdateDepartment(department);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Test]
        public async Task InsertOrUpdateDepartment_Returns500WhenDepartmentNameIsEmpty()
        {
            // Arrange
            var department = new Department {DepartmentId = Guid.NewGuid(), DepartmentName = "" };
            _mockDepartmentManager.Setup(x => x.InsertOrUpdateDepartmentAsync(department))
                .ThrowsAsync(new ArgumentException("Department name cannot be empty"));

            // Act
            var result = await _controller.InsertOrUpdateDepartment(department);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        #endregion
    }
}
