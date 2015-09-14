using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace TripService.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private static readonly User RegisteredUser = new User();
        private static readonly User Guest = null;
        private static readonly Trip ToLondon = new Trip();
        private static readonly Trip ToNewYork = new Trip();
        private static readonly User UserA = new User();
        private TripService _TripService;
        private readonly Mock<TripDao> _TripDao = new Mock<TripDao>();

        [SetUp]
        public void Init()
        {
            _TripService = new TripService(_TripDao.Object);
        }

        [Test]
        [ExpectedException(typeof (UserNotLoggedInException))]
        public void ThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            _TripService.GetFriendTrips(UserA, Guest);
        }

        [Test]
        public void NotReturnAnyTripIfUsersAreNotFriends()
        {
            var friend = UserBuilder.CreateUser()
                .WithFriends(new[] {UserA})
                .WithTrips(new[] {ToLondon, ToNewYork})
                .Build();
            IList<Trip> tripList = _TripService.GetFriendTrips(friend, RegisteredUser);
            Assert.That(tripList.Count, Is.EqualTo(0));
        }

        [Test]
        public void ReturnTripsIfUsersAreFriend()
        {
            var trips = new List<Trip>{ToLondon, ToNewYork};
            var friend =
                UserBuilder.CreateUser()
                .WithFriends(new[] { RegisteredUser })
                .WithTrips(trips).Build();

            _TripDao.Setup(a => a.TripsByUser(friend)).Returns(trips);
            var tripList = _TripService.GetFriendTrips(friend, RegisteredUser);
            
            Assert.That(tripList.Count, Is.EqualTo(2));
        }

    }
}