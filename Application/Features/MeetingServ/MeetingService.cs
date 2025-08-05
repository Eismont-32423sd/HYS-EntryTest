using Application.Services.Interfaces;
using Domain.Entities;
using Infrastracture.Services.MeetingScheduler;

namespace Application.Features.MeetingServ
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingSchedulerServic _scheduler;

        public MeetingService(IMeetingSchedulerServic scheduler)
        {
            _scheduler = scheduler;
        }
        public Meeting? ScheduleMeeting(List<int> participantIds, TimeSpan duration, 
            DateTime earliestStart, DateTime latestEnd)
        {
            return _scheduler.CreateMeeting(participantIds, duration, earliestStart, latestEnd); ;
        }
    }
}
