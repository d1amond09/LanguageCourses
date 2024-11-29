using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.Employees;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;

namespace LanguageCourses.Tests.Handlers.Employees
{
    public class DeleteJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly DeleteEmployeeHandler _handler;

        public DeleteJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _handler = new DeleteEmployeeHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_EmployeeExists_DeletesEmployeeAndReturnsResponse()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { Id = employeeId };

            var command = new DeleteEmployeeCommand(employeeId, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync(employee);
            _mockRepo.Setup(r => r.Employees.DeleteEmployee(employee));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<Employee>>(result);
            var okResponse = result as ApiOkResponse<Employee>;
            Assert.Equal(employee, okResponse.Result);
            _mockRepo.Verify(r => r.Employees.DeleteEmployee(employee), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_EmployeeDoesNotExist_ReturnsEmployeeNotFoundResponse()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var command = new DeleteEmployeeCommand(employeeId, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync((Employee)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<EmployeeNotFoundResponse>(result);
            var notFoundResponse = result as EmployeeNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(employeeId.ToString()));
            _mockRepo.Verify(r => r.Employees.DeleteEmployee(It.IsAny<Employee>()), Times.Never);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ThrowsException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { Id = employeeId };
            var command = new DeleteEmployeeCommand(employeeId, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync(employee);
            _mockRepo.Setup(r => r.Employees.DeleteEmployee(employee));
            _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(r => r.Employees.DeleteEmployee(employee), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}