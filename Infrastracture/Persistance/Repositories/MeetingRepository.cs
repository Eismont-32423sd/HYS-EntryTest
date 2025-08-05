using Domain.Entities;
using Domain.Interfaces;

namespace Infrastracture.Persistance.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly List<Meeting> _meetingList = [];
        private int _nextId = 1;
        public Meeting AddMeeting(Meeting meeting)
        {
            meeting.Id = _nextId++;
            _meetingList.Add(meeting);
            return meeting;
        }

        public IEnumerable<Meeting> GetAllMeetings()
        {
            return _meetingList;
        }

        public IEnumerable<Meeting> GetByUserId(int userId)
        {
            return _meetingList.Where(m => m.Participants.Contains(userId));
        }

        public Meeting GetMeetingById(int id)
        {
            return _meetingList.FirstOrDefault(m => m.Id == id)!;
        }
    }
}
