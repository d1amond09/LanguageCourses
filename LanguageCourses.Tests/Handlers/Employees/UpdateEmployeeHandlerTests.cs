using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.Employees;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LanguageCourses.Tests.Handlers.Employees
{
    public class UpdateJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateEmployeeHandler _handler;

        public UpdateJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateEmployeeHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_EmployeeExists_UpdatesEmployeeAndReturnsResponse()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var existingEmployee = new Employee { Id = employeeId };
            var updatedEmployeeDto = new EmployeeForUpdateDto {  };
            var command = new UpdateEmployeeCommand(employeeId, updatedEmployeeDto, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync(existingEmployee);
            _mockMapper.Setup(m => m.Map(updatedEmployeeDto, existingEmployee)); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<Employee>>(result);
            var okResponse = result as ApiOkResponse<Employee>;
            Assert.Equal(existingEmployee, okResponse.Result);
            _mockRepo.Verify(r => r.Employees.GetEmployeeAsync(employeeId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(updatedEmployeeDto, existingEmployee), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_EmployeeDoesNotExist_ReturnsEmployeeNotFoundResponse()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var updatedEmployeeDto = new EmployeeForUpdateDto {  };
            var command = new UpdateEmployeeCommand(employeeId, updatedEmployeeDto, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync((Employee)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<EmployeeNotFoundResponse>(result);
            var notFoundResponse = result as EmployeeNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(employeeId.ToString()));
            _mockRepo.Verify(r => r.Employees.GetEmployeeAsync(employeeId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(It.IsAny<EmployeeDto>(), It.IsAny<Employee>()), Times.Never);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ThrowsException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var existingEmployee = new Employee { Id = employeeId };
            var updatedEmployeeDto = new EmployeeForUpdateDto { /* заполните данными */ };
            var command = new UpdateEmployeeCommand(employeeId, updatedEmployeeDto, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync(existingEmployee);
            _mockMapper.Setup(m => m.Map(updatedEmployeeDto, existingEmployee));
            _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(r => r.Employees.GetEmployeeAsync(employeeId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(updatedEmployeeDto, existingEmployee), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}