using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.Courses;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;

namespace LanguageCourses.Tests.Handlers.Courses;

public class CreateCourseHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateCourseHandler _handler;

    public CreateCourseHandlerTests()
    {
        _mockRepo = new Mock<IRepositoryManager>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateCourseHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesCourseAndReturnsResponse()
    {
        // Arrange
        var courseToCreate = new CourseForCreationDto { };
        var courseDto = new CourseDto {  };
        var courseEntity = new Course {  };
        var expectedResponse = new ApiOkResponse<CourseDto>(courseDto);

        var command = new CreateCourseCommand( courseToCreate );

        _mockMapper.Setup(m => m.Map<Course>(courseToCreate)).Returns(courseEntity);
        _mockRepo.Setup(r => r.Courses.CreateCourse(courseEntity));
        _mockMapper.Setup(m => m.Map<CourseDto>(courseEntity)).Returns(courseDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<ApiOkResponse<CourseDto>>(result);
        var okResponse = result as ApiOkResponse<CourseDto>;
        Assert.Equal(courseDto, okResponse.Result);
        _mockRepo.Verify(r => r.Courses.CreateCourse(courseEntity), Times.Once);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_SaveAsyncFails_ReturnsErrorResponse()
    {
        // Arrange
        var courseToCreate = new CourseForCreationDto {  };
        var courseDto = new CourseDto { };
        var courseEntity = new Course { };

        var command = new CreateCourseCommand (courseToCreate);

        _mockMapper.Setup(m => m.Map<Course>(courseToCreate)).Returns(courseEntity);
        _mockRepo.Setup(r => r.Courses.CreateCourse(courseEntity));
        _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        _mockRepo.Verify(r => r.Courses.CreateCourse(courseEntity), Times.Once);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
    }
}