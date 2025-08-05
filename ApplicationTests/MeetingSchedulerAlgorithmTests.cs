using Domain.Entities;
using Infrastracture.Services.MeetingScheduler;

namespace ApplicationTests
{
    public class MeetingSchedulerAlgorithmTests
    {
        [Fact]
        public void ReturnsEarliestStart_WhenNoMeetings()
        {
            var userMeetings = new List<List<Meeting>> { new(), new() };
            var start = new DateTime(2025, 8, 4, 10, 0, 0);
            var end = start.Date.AddHours(17);

            var slot = MeetingSchedulerAlgorithm.FindEarliestAvailableSlot(
                userMeetings,
                TimeSpan.FromMinutes(30),
                start,
                end
            );

            Assert.Equal(start, slot);
        }

        [Fact]
        public void SkipsOverlappingMeetings()
        {
            var m1 = new Meeting { StartTime = new DateTime(2025, 8, 4, 10, 0, 0), EndTime = new DateTime(2025, 8, 4, 11, 0, 0) };
            var m2 = new Meeting { StartTime = new DateTime(2025, 8, 4, 10, 30, 0), EndTime = new DateTime(2025, 8, 4, 11, 30, 0) };

            var userMeetings = new List<List<Meeting>>
        {
            new() { m1 },
            new() { m2 }
        };

            var start = new DateTime(2025, 8, 4, 10, 0, 0);
            var end = new DateTime(2025, 8, 4, 17, 0, 0);

            var slot = MeetingSchedulerAlgorithm.FindEarliestAvailableSlot(
                userMeetings,
                TimeSpan.FromMinutes(30),
                start,
                end
            );

            Assert.Equal(new DateTime(2025, 8, 4, 11, 30, 0), slot);
        }

        [Fact]
        public void ReturnsNull_WhenNoAvailableSlot()
        {
            var m = new Meeting { StartTime = new DateTime(2025, 8, 4, 9, 0, 0), EndTime = new DateTime(2025, 8, 4, 17, 0, 0) };
            var userMeetings = new List<List<Meeting>> { new() { m } };

            var start = new DateTime(2025, 8, 4, 9, 0, 0);
            var end = new DateTime(2025, 8, 4, 17, 0, 0);

            var slot = MeetingSchedulerAlgorithm.FindEarliestAvailableSlot(
                userMeetings,
                TimeSpan.FromHours(1),
                start,
                end
            );

            Assert.Null(slot);
        }

        [Fact]
        public void RespectsBusinessHours()
        {
            var userMeetings = new List<List<Meeting>> { new() };
            var start = new DateTime(2025, 8, 4, 7, 0, 0);
            var end = new DateTime(2025, 8, 4, 20, 0, 0); 

            var slot = MeetingSchedulerAlgorithm.FindEarliestAvailableSlot(
                userMeetings,
                TimeSpan.FromMinutes(30),
                start,
                end
            );

            Assert.Equal(new DateTime(2025, 8, 4, 9, 0, 0), slot);
        }
    }
}