using Application.Services.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HYS_Test.Controllers
{
    [ApiController]
    [Route("meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost]
        public IActionResult ScheduleMeeting([FromBody] MeetingRequest request)
        {
            var meeting = _meetingService.ScheduleMeeting(
                request.ParticipantIds,
                TimeSpan.FromMinutes(request.DurationMinutes),
                request.EarliestStart,
                request.LatestEnd
            );

            if (meeting == null)
                return Conflict("No available time slot found.");

            return Ok(meeting);
        }
    }
}
