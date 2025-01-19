using AutoMapper;
using Contracts;
using LanguageCourses.Application.Handlers.Employees;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;

namespace LanguageCourses.Tests.Handlers.Employees
{
    public class GetJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetEmployeeHandler _handler;

        public GetJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetEmployeeHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_EmployeeExists_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { Id = employeeId }; // Предположим, что у вас есть класс Employee
            var employeeDto = new EmployeeDto { /* заполните данными */ };
            var query = new GetEmployeeQuery(employeeId, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync(employee);
            _mockMapper.Setup(m => m.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<EmployeeDto>>(result);
            var okResponse = result as ApiOkResponse<EmployeeDto>;
            Assert.Equal(employeeDto, okResponse.Result);
            _mockRepo.Verify(r => r.Employees.GetEmployeeAsync(employeeId, false), Times.Once);
            _mockMapper.Verify(m => m.Map<EmployeeDto>(employee), Times.Once);
        }

        [Fact]
        public async Task Handle_EmployeeDoesNotExist_ReturnsEmployeeNotFoundResponse()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var query = new GetEmployeeQuery(employeeId, false);

            _mockRepo.Setup(r => r.Employees.GetEmployeeAsync(employeeId, false)).ReturnsAsync((Employee)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<EmployeeNotFoundResponse>(result);
            var notFoundResponse = result as EmployeeNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(employeeId.ToString()));
            _mockRepo.Verify(r => r.Employees.GetEmployeeAsync(employeeId, false), Times.Once);
            _mockMapper.Verify(m => m.Map<EmployeeDto>(It.IsAny<Employee>()), Times.Never);
        }
    }
}