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

namespace LanguageCourses.Tests.Handlers.Employees;

public class CreateJobTitleHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateEmployeeHandler _handler;

    public CreateJobTitleHandlerTests()
    {
        _mockRepo = new Mock<IRepositoryManager>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateEmployeeHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesEmployeeAndReturnsResponse()
    {
        // Arrange
        var employeeToCreate = new EmployeeForCreationDto {  };
        var employeeDto = new EmployeeDto {  };
        var employeeEntity = new Employee {  };
        var expectedResponse = new ApiOkResponse<EmployeeDto>(employeeDto);

        var command = new CreateEmployeeCommand(employeeToCreate);

        _mockMapper.Setup(m => m.Map<Employee>(employeeToCreate)).Returns(employeeEntity);
        _mockRepo.Setup(r => r.Employees.CreateEmployee(employeeEntity));
        _mockMapper.Setup(m => m.Map<EmployeeDto>(employeeEntity)).Returns(employeeDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<ApiOkResponse<EmployeeDto>>(result);
        var okResponse = result as ApiOkResponse<EmployeeDto>;
        Assert.Equal(employeeDto, okResponse.Result);
        _mockRepo.Verify(r => r.Employees.CreateEmployee(employeeEntity), Times.Once);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_SaveAsyncFails_ReturnsErrorResponse()
    {
        // Arrange
        var employeeToCreate = new EmployeeForCreationDto {  };
        var employeeEntity = new Employee { };

        var command = new CreateEmployeeCommand(employeeToCreate);

        _mockMapper.Setup(m => m.Map<Employee>(employeeToCreate)).Returns(employeeEntity);
        _mockRepo.Setup(r => r.Employees.CreateEmployee(employeeEntity));
        _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        _mockRepo.Verify(r => r.Employees.CreateEmployee(employeeEntity), Times.Once);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
    }
}