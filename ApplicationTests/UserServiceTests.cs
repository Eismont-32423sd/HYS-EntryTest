using Application.Features.UserServ;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace ApplicationTests
{
    public class UserServiceTests
    {
        [Fact]
        public void CreateUser_CallsRepositoryAndReturnsUser()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserRepo = new Mock<IUserRepository>();

            var user = new User { Id = 1, Name = "Alice" };

            mockUserRepo.Setup(r => r.AddUser(user)).Returns(user);
            mockUnitOfWork.Setup(u => u.UserRepository).Returns(mockUserRepo.Object);

            var service = new UserService(mockUnitOfWork.Object);

            var result = service.CreateUser(user);

            Assert.Equal(user, result);
            mockUserRepo.Verify(r => r.AddUser(user), Times.Once);
        }

        [Fact]
        public void GetMeetingList_CallsRepositoryAndReturnsMeetings()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMeetingRepo = new Mock<IMeetingRepository>();

            var meetings = new List<Meeting>
        {
            new Meeting { Id = 1, Participants = new List<int>{ 1 } },
            new Meeting { Id = 2, Participants = new List<int>{ 1, 2 } }
        };

            mockMeetingRepo.Setup(r => r.GetByUserId(1)).Returns(meetings);
            mockUnitOfWork.Setup(u => u.MeetingRepository).Returns(mockMeetingRepo.Object);

            var service = new UserService(mockUnitOfWork.Object);
            var result = service.GetMeetingList(1);

            Assert.Equal(meetings, result);
            mockMeetingRepo.Verify(r => r.GetByUserId(1), Times.Once);
        }
    }
}
