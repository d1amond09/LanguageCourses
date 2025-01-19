using AutoMapper;
using Contracts;
using LanguageCourses.Application.Handlers.Courses;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;

namespace LanguageCourses.Tests.Handlers.Courses
{
    public class GetEmployeeHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetCourseHandler _handler;

        public GetEmployeeHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetCourseHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_CourseExists_ReturnsCourseDto()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId }; // Предположим, что у вас есть класс Course
            var courseDto = new CourseDto { /* заполните данными */ };
            var query = new GetCourseQuery(courseId, false);

            _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync(course);
            _mockMapper.Setup(m => m.Map<CourseDto>(course)).Returns(courseDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<CourseDto>>(result);
            var okResponse = result as ApiOkResponse<CourseDto>;
            Assert.Equal(courseDto, okResponse.Result);
            _mockRepo.Verify(r => r.Courses.GetCourseAsync(courseId, false), Times.Once);
            _mockMapper.Verify(m => m.Map<CourseDto>(course), Times.Once);
        }

        [Fact]
        public async Task Handle_CourseDoesNotExist_ReturnsCourseNotFoundResponse()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var query = new GetCourseQuery(courseId, false);

            _mockRepo.Setup(r => r.Courses.GetCourseAsync(courseId, false)).ReturnsAsync((Course)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<CourseNotFoundResponse>(result);
            var notFoundResponse = result as CourseNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(courseId.ToString()));
            _mockRepo.Verify(r => r.Courses.GetCourseAsync(courseId, false), Times.Once);
            _mockMapper.Verify(m => m.Map<CourseDto>(It.IsAny<Course>()), Times.Never);
        }
    }
}