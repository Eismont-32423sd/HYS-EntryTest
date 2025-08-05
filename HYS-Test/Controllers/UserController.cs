using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HYS_Test.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            var createdUser = _userService.CreateUser(user);
            return Ok(createdUser);
        }

        [HttpGet("{userId}/meetings")]
        public ActionResult<IEnumerable<Meeting>> GetMeetings(int userId)
        {
            var meetings = _userService.GetMeetingList(userId);
            return Ok(meetings);
        }
    }
}
