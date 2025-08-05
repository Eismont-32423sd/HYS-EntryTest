using Domain.Entities;
using Infrastracture.Persistance.Repositories;

namespace InfrastractureTests
{
    public class MeetingRepositoryTests
    {
        [Fact]
        public void AddMeeting_AssignsIdAndStoresMeeting()
        {
            var repo = new MeetingRepository();
            var meeting = new Meeting
            {
                Participants = new List<int> { 1, 2 },
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1)
            };

            var added = repo.AddMeeting(meeting);

            Assert.Equal(1, added.Id);
            Assert.Contains(added, repo.GetAllMeetings());
        }

        [Fact]
        public void GetAllMeetings_ReturnsAllAddedMeetings()
        {
            var repo = new MeetingRepository();

            repo.AddMeeting(new Meeting { Participants = new List<int> { 1 }, 
                StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) });

            repo.AddMeeting(new Meeting { Participants = new List<int> { 2 }, 
                StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) });

            var all = repo.GetAllMeetings();

            Assert.Equal(2, all.Count());
        }

        [Fact]
        public void GetByUserId_ReturnsOnlyUserMeetings()
        {
            var repo = new MeetingRepository();
            repo.AddMeeting(new Meeting { Participants = new List<int> { 1, 2 }, 

                StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) });

            repo.AddMeeting(new Meeting { Participants = new List<int> { 3 }, 
                StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) });

            var userMeetings = repo.GetByUserId(1);

            Assert.Single(userMeetings);
            Assert.Contains(userMeetings, m => m.Participants.Contains(1));
        }

        [Fact]
        public void GetMeetingById_ReturnsCorrectMeeting()
        {
            var repo = new MeetingRepository();
            var meeting = repo.AddMeeting(new Meeting { Participants = new List<int> { 1 }, 
                StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) });

            var found = repo.GetMeetingById(meeting.Id);

            Assert.NotNull(found);
            Assert.Equal(meeting.Id, found.Id);
        }

        [Fact]
        public void GetMeetingById_ReturnsNullIfNotFound()
        {
            var repo = new MeetingRepository();

            var result = repo.GetMeetingById(999);

            Assert.Null(result);
        }
    }
}