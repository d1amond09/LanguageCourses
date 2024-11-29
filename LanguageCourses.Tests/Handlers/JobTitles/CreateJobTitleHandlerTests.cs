using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.JobTitles;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;

namespace LanguageCourses.Tests.Handlers.JobTitles
{
    public class CreateJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateJobTitleHandler _handler;

        public CreateJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateJobTitleHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_CreatesJobTitleAndReturnsResponse()
        {
            // Arrange
            var jobTitleToCreate = new JobTitleForCreationDto { };
            var jobTitleDto = new JobTitleDto { };
            var jobTitleEntity = new JobTitle { };
            var expectedResponse = new ApiOkResponse<JobTitleDto>(jobTitleDto);

            var command = new CreateJobTitleCommand(jobTitleToCreate);

            _mockMapper.Setup(m => m.Map<JobTitle>(jobTitleToCreate)).Returns(jobTitleEntity);
            _mockRepo.Setup(r => r.JobTitles.CreateJobTitle(jobTitleEntity));
            _mockMapper.Setup(m => m.Map<JobTitleDto>(jobTitleEntity)).Returns(jobTitleDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<JobTitleDto>>(result);
            var okResponse = result as ApiOkResponse<JobTitleDto>;
            Assert.Equal(jobTitleDto, okResponse.Result);
            _mockRepo.Verify(r => r.JobTitles.CreateJobTitle(jobTitleEntity), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ReturnsErrorResponse()
        {
            // Arrange
            var jobTitleToCreate = new JobTitleForCreationDto { };
            var jobTitleEntity = new JobTitle { };

            var command = new CreateJobTitleCommand(jobTitleToCreate);

            _mockMapper.Setup(m => m.Map<JobTitle>(jobTitleToCreate)).Returns(jobTitleEntity);
            _mockRepo.Setup(r => r.JobTitles.CreateJobTitle(jobTitleEntity));
            _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(r => r.JobTitles.CreateJobTitle(jobTitleEntity), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}