namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMeetingRepository MeetingRepository { get; }
        void SaveChanges();
    }
}
