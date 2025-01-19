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
    public class UpdateJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateJobTitleHandler _handler;

        public UpdateJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateJobTitleHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_JobTitleExists_UpdatesJobTitleAndReturnsResponse()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var existingJobTitle = new JobTitle { Id = jobTitleId };
            var updatedJobTitleDto = new JobTitleForUpdateDto { };
            var command = new UpdateJobTitleCommand(jobTitleId, updatedJobTitleDto, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync(existingJobTitle);
            _mockMapper.Setup(m => m.Map(updatedJobTitleDto, existingJobTitle));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<JobTitle>>(result);
            var okResponse = result as ApiOkResponse<JobTitle>;
            Assert.Equal(existingJobTitle, okResponse.Result);
            _mockRepo.Verify(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(updatedJobTitleDto, existingJobTitle), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_JobTitleDoesNotExist_ReturnsJobTitleNotFoundResponse()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var updatedJobTitleDto = new JobTitleForUpdateDto { };
            var command = new UpdateJobTitleCommand(jobTitleId, updatedJobTitleDto, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync((JobTitle)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<JobTitleNotFoundResponse>(result);
            var notFoundResponse = result as JobTitleNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(jobTitleId.ToString()));
            _mockRepo.Verify(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(It.IsAny<JobTitleDto>(), It.IsAny<JobTitle>()), Times.Never);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ThrowsException()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var existingJobTitle = new JobTitle { Id = jobTitleId };
            var updatedJobTitleDto = new JobTitleForUpdateDto { };
            var command = new UpdateJobTitleCommand(jobTitleId, updatedJobTitleDto, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync(existingJobTitle);
            _mockMapper.Setup(m => m.Map(updatedJobTitleDto, existingJobTitle));
            _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false), Times.Once);
            _mockMapper.Verify(m => m.Map(updatedJobTitleDto, existingJobTitle), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}