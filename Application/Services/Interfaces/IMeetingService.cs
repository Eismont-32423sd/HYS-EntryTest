using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IMeetingService
    {
        Meeting? ScheduleMeeting(List<int> participantIds, 
            TimeSpan duration, DateTime earliestStart, DateTime latestEnd);
    }
}
