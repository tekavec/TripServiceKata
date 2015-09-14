using NUnit.Framework;

namespace TripService.Tests.UnitTests
{
    [TestFixture]
    public class UserShould
    {
        private User _Steve = new User();
        private User _John = new User();

        [Test]
        public void InformIfUsersAreNotFriends()
        {
            var user = UserBuilder.CreateUser()
                .WithFriends(new[] { _Steve }).Build();
            Assert.That(user.IsFriendWith(_John), Is.EqualTo(false));
        }

        [Test]
        public void InformIfUsersAreFriends()
        {
            var user = UserBuilder.CreateUser()
                .WithFriends(new[] {_Steve}).Build();
            Assert.That(user.IsFriendWith(_Steve), Is.EqualTo(true));
        }
    }
}