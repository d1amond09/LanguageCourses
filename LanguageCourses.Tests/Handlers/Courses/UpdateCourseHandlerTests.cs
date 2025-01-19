using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.Courses;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;

namespace LanguageCourses.Tests.Handlers.Courses
{
    public class UpdateEmployeeHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateCourseHandler _handler;

        public UpdateEmployeeHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateCourseHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_CourseExists_UpdatesCourseAndReturnsResponse()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var existingCourse = new Course { Id = courseId };
            var updatedCourseDto = new CourseForUpdateDto { };
            var command = new UpdateCourseCommand(courseId, updatedCourseDto, false);

            _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync(existingCourse);
            _mockMapper.Setup(m => m.Map(updatedCourseDto, existingCourse));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<Course>>(result);
            var okResponse = result as ApiOkResponse<Course>;
            Assert.Equal(existingCourse, okResponse.Result);
            _mockRepo.Verify(r => r.Courses.GetCourseAsync(courseId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(updatedCourseDto, existingCourse), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_CourseDoesNotExist_ReturnsCourseNotFoundResponse()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var updatedCourseDto = new CourseForUpdateDto { };
            var command = new UpdateCourseCommand(courseId, updatedCourseDto, false);

            _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync((Course)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<CourseNotFoundResponse>(result);
            var notFoundResponse = result as CourseNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(courseId.ToString()));
            _mockRepo.Verify(r => r.Courses.GetCourseAsync(courseId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(It.IsAny<CourseDto>(), It.IsAny<Course>()), Times.Never);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ThrowsException()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var existingCourse = new Course { Id = courseId };
            var updatedCourseDto = new CourseForUpdateDto { };
            var command = new UpdateCourseCommand(courseId, updatedCourseDto, false);

            _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync(existingCourse);
            _mockMapper.Setup(m => m.Map(updatedCourseDto, existingCourse));
            _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(r => r.Courses.GetCourseAsync(courseId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(updatedCourseDto, existingCourse), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}