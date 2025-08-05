using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastracture.Services.MeetingScheduler
{
    public class MeetingSchedulerService : IMeetingSchedulerServic
    {
        private readonly IUnitOfWork _unitOfWork;

        public MeetingSchedulerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DateTime? ScheduleMeeting(List<int> participantIds, TimeSpan duration,
            DateTime earliestStart, DateTime latestEnd)
        {
            var userMeetings = participantIds
                .Select(id => _unitOfWork.MeetingRepository.GetByUserId(id).ToList())
                .ToList();

            return MeetingSchedulerAlgorithm.FindEarliestAvailableSlot(userMeetings,
                duration, earliestStart, latestEnd);
        }

        public Meeting? CreateMeeting(List<int> participantIds, TimeSpan duration,
            DateTime earliestStart, DateTime latestEnd)
        {
            var startTime = ScheduleMeeting(participantIds, duration, earliestStart, latestEnd);
            if (startTime == null) return null;

            var endTime = startTime.Value.Add(duration);

            var meeting = new Meeting
            {
                Participants = participantIds,
                StartTime = startTime.Value,
                EndTime = endTime
            };

            return _unitOfWork.MeetingRepository.AddMeeting(meeting);
        }
    }
}
