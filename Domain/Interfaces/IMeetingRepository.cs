using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMeetingRepository
    {
        Meeting AddMeeting(Meeting meeting);
        Meeting GetMeetingById(int id);
        IEnumerable<Meeting> GetAllMeetings();
        IEnumerable<Meeting> GetByUserId(int userId);
    }
}
