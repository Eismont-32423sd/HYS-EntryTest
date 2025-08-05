using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Features.UserServ
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User CreateUser(User user)
        {
            return _unitOfWork.UserRepository.AddUser(user);
        }

        public IEnumerable<Meeting> GetMeetingList(int userId)
        {
            return _unitOfWork.MeetingRepository.GetByUserId(userId);
        }
    }
}
