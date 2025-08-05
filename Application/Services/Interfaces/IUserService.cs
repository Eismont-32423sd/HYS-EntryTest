using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(User user);
        IEnumerable<Meeting> GetMeetingList(int userId);
    }
}
