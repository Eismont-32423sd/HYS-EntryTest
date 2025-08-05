using Domain.Interfaces;
using Infrastracture.Persistance.Repositories;

namespace Infrastracture.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserRepository _userRepository;
        private MeetingRepository _meetingRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository();
                }
                return _userRepository;
            }
        }
        public IMeetingRepository MeetingRepository
        {
            get
            {
                if (_meetingRepository == null)
                {
                    _meetingRepository = new MeetingRepository();
                }
                return _meetingRepository;
            }
        }
        public void SaveChanges()
        {

        }
    }
}
