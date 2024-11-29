using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.Courses;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LanguageCourses.Tests.Handlers.Courses;

public class DeleteEmployeeHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly DeleteCourseHandler _handler;

    public DeleteEmployeeHandlerTests()
    {
        _mockRepo = new Mock<IRepositoryManager>();
        _handler = new DeleteCourseHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_CourseExists_DeletesCourseAndReturnsResponse()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var course = new Course { Id = courseId };

        var command = new DeleteCourseCommand(courseId, false);

        _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync(course);
        _mockRepo.Setup(r => r.Courses.DeleteCourse(course));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<ApiOkResponse<Course>>(result);
        var okResponse = result as ApiOkResponse<Course>;
        Assert.Equal(course, okResponse.Result);
        _mockRepo.Verify(r => r.Courses.DeleteCourse(course), Times.Once);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_CourseDoesNotExist_ReturnsCourseNotFoundResponse()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var command = new DeleteCourseCommand(courseId, false);

        _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync((Course)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<CourseNotFoundResponse>(result);
        var notFoundResponse = result as CourseNotFoundResponse;
        Assert.True(notFoundResponse.Message.Contains(courseId.ToString()));
        _mockRepo.Verify(r => r.Courses.DeleteCourse(It.IsAny<Course>()), Times.Never);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_SaveAsyncFails_ThrowsException()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var course = new Course { Id = courseId };
        var command = new DeleteCourseCommand(courseId, false);

        _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync(course);
        _mockRepo.Setup(r => r.Courses.DeleteCourse(course));
        _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        _mockRepo.Verify(r => r.Courses.DeleteCourse(course), Times.Once);
        _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
    }
}