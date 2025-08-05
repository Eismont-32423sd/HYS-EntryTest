using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IMeetingSchedulerServic
    {
        Meeting? CreateMeeting(List<int> participantIds, TimeSpan duration, DateTime earliestStart, DateTime latestEnd);
    }
}
