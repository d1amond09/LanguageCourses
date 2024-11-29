using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Handlers.JobTitles;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LanguageCourses.Tests.Handlers.JobTitles
{
    public class DeleteJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly DeleteJobTitleHandler _handler;

        public DeleteJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _handler = new DeleteJobTitleHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_JobTitleExists_DeletesJobTitleAndReturnsResponse()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var jobTitle = new JobTitle { Id = jobTitleId };

            var command = new DeleteJobTitleCommand(jobTitleId, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync(jobTitle);
            _mockRepo.Setup(r => r.JobTitles.DeleteJobTitle(jobTitle));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<JobTitle>>(result);
            var okResponse = result as ApiOkResponse<JobTitle>;
            Assert.Equal(jobTitle, okResponse.Result);
            _mockRepo.Verify(r => r.JobTitles.DeleteJobTitle(jobTitle), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_JobTitleDoesNotExist_ReturnsJobTitleNotFoundResponse()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var command = new DeleteJobTitleCommand(jobTitleId, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync((JobTitle)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<JobTitleNotFoundResponse>(result);
            var notFoundResponse = result as JobTitleNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(jobTitleId.ToString()));
            _mockRepo.Verify(r => r.JobTitles.DeleteJobTitle(It.IsAny<JobTitle>()), Times.Never);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ThrowsException()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var jobTitle = new JobTitle { Id = jobTitleId };
            var command = new DeleteJobTitleCommand(jobTitleId, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync(jobTitle);
            _mockRepo.Setup(r => r.JobTitles.DeleteJobTitle(jobTitle));
            _mockRepo.Setup(r => r.SaveAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(r => r.JobTitles.DeleteJobTitle(jobTitle), Times.Once);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}