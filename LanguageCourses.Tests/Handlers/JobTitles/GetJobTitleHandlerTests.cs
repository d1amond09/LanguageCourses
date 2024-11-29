using AutoMapper;
using Contracts;
using LanguageCourses.Application.Queries;
using LanguageCourses.Application.Handlers.JobTitles;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LanguageCourses.Tests.Handlers.JobTitles
{
    public class GetJobTitleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetJobTitleHandler _handler;

        public GetJobTitleHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetJobTitleHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_JobTitleExists_ReturnsJobTitleDto()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var jobTitle = new JobTitle { Id = jobTitleId }; // Предположим, что у вас есть класс JobTitle
            var jobTitleDto = new JobTitleDto { /* заполните данными */ };
            var query = new GetJobTitleQuery(jobTitleId, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync(jobTitle);
            _mockMapper.Setup(m => m.Map<JobTitleDto>(jobTitle)).Returns(jobTitleDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<ApiOkResponse<JobTitleDto>>(result);
            var okResponse = result as ApiOkResponse<JobTitleDto>;
            Assert.Equal(jobTitleDto, okResponse.Result);
            _mockRepo.Verify(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false), Times.Once);
            _mockMapper.Verify(m => m.Map<JobTitleDto>(jobTitle), Times.Once);
        }

        [Fact]
        public async Task Handle_JobTitleDoesNotExist_ReturnsJobTitleNotFoundResponse()
        {
            // Arrange
            var jobTitleId = Guid.NewGuid();
            var query = new GetJobTitleQuery(jobTitleId, false);

            _mockRepo.Setup(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false)).ReturnsAsync((JobTitle)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<JobTitleNotFoundResponse>(result);
            var notFoundResponse = result as JobTitleNotFoundResponse;
            Assert.True(notFoundResponse.Message.Contains(jobTitleId.ToString()));
            _mockRepo.Verify(r => r.JobTitles.GetJobTitleAsync(jobTitleId, false), Times.Once);
            _mockMapper.Verify(m => m.Map<JobTitleDto>(It.IsAny<JobTitle>()), Times.Never);
        }
    }
}