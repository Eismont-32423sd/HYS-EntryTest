using Application.Features.MeetingServ;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastracture.Services.MeetingScheduler;
using Moq;

namespace ApplicationTests
{
    public class MeetingServiceTests
    {
        [Fact]
        public void ScheduleMeeting_DelegatesToScheduler()
        {
            var mockScheduler = new Mock<IMeetingSchedulerServic>();
            var service = new MeetingService(mockScheduler.Object);

            var participantIds = new List<int> { 1, 2 };
            var duration = TimeSpan.FromMinutes(60);
            var earliestStart = DateTime.UtcNow;
            var latestEnd = earliestStart.AddHours(8);

            var expectedMeeting = new Meeting
            {
                Id = 1,
                Participants = participantIds,
                StartTime = earliestStart.AddHours(1),
                EndTime = earliestStart.AddHours(2)
            };

            mockScheduler
                .Setup(s => s.CreateMeeting(participantIds, duration, earliestStart, latestEnd))
                .Returns(expectedMeeting);

            var result = service.ScheduleMeeting(participantIds, duration, earliestStart, latestEnd);

            Assert.Equal(expectedMeeting, result);
            mockScheduler.Verify(s => s.CreateMeeting(participantIds, duration, earliestStart, latestEnd), Times.Once);
        }
    }
}
