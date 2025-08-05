using Domain.Entities;
using Domain.Interfaces;
using Infrastracture.Services.MeetingScheduler;
using Moq;

namespace ApplicationTests
{
    public class MeetingSchedulerServiceTests
    {
        [Fact]
        public void ScheduleMeeting_ReturnsEarliestAvailableSlot()
        {
            var mockMeetingRepo = new Mock<IMeetingRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockMeetingRepo.Setup(m => m.GetByUserId(It.IsAny<int>()))
                .Returns(new List<Meeting>());

            mockUnitOfWork.Setup(u => u.MeetingRepository).Returns(mockMeetingRepo.Object);

            var service = new MeetingSchedulerService(mockUnitOfWork.Object);

            var participantIds = new List<int> { 1, 2 };
            var earliestStart = new DateTime(2025, 8, 4, 9, 0, 0);
            var latestEnd = new DateTime(2025, 8, 4, 17, 0, 0);
            var duration = TimeSpan.FromMinutes(30);

            var result = service.ScheduleMeeting(participantIds, duration, earliestStart, latestEnd);

            Assert.Equal(earliestStart, result);
        }

        [Fact]
        public void CreateMeeting_ReturnsNull_WhenNoAvailableSlot()
        {
            var mockMeetingRepo = new Mock<IMeetingRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockMeetingRepo.Setup(m => m.GetByUserId(It.IsAny<int>()))
                .Returns(new List<Meeting> { new Meeting { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(8) } });

            mockUnitOfWork.Setup(u => u.MeetingRepository).Returns(mockMeetingRepo.Object);

            var service = new MeetingSchedulerService(mockUnitOfWork.Object);

            var participantIds = new List<int> { 1 };
            var earliestStart = DateTime.Now;
            var latestEnd = DateTime.Now.AddHours(8);
            var duration = TimeSpan.FromHours(1);

            var result = service.CreateMeeting(participantIds, duration, earliestStart, latestEnd);

            Assert.Null(result);
        }

        [Fact]
        public void CreateMeeting_AddsMeeting_WhenSlotAvailable()
        {
            var mockMeetingRepo = new Mock<IMeetingRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockMeetingRepo.Setup(m => m.GetByUserId(It.IsAny<int>())).Returns(new List<Meeting>());

            mockMeetingRepo.Setup(m => m.AddMeeting(It.IsAny<Meeting>())).Returns((Meeting m) => m);

            mockUnitOfWork.Setup(u => u.MeetingRepository).Returns(mockMeetingRepo.Object);

            var service = new MeetingSchedulerService(mockUnitOfWork.Object);

            var participantIds = new List<int> { 1, 2 };
            var earliestStart = new DateTime(2025, 8, 4, 9, 0, 0);
            var latestEnd = new DateTime(2025, 8, 4, 17, 0, 0);
            var duration = TimeSpan.FromMinutes(30);

            var meeting = service.CreateMeeting(participantIds, duration, earliestStart, latestEnd);

            Assert.NotNull(meeting);
            Assert.Equal(participantIds, meeting.Participants);
            Assert.Equal(duration, meeting.EndTime - meeting.StartTime);

            mockMeetingRepo.Verify(m => m.AddMeeting(It.IsAny<Meeting>()), Times.Once);
        }
    }
}
