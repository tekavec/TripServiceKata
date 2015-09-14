using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace TripService.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private static User _RegisteredUser = new User();
        private static User _Guest = null;
        private static Trip _ToLondon = new Trip();
        private static Trip _ToNewYork = new Trip();
        private static User _UserA = new User();
        private TripService _TripService;
        private Mock<TripDao> _TripDao = new Mock<TripDao>();

        [SetUp]
        public void Init()
        {
            _TripService = new TripService(_TripDao.Object);
        }

        [Test]
        [ExpectedException(typeof (UserNotLoggedInException))]
        public void ThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            _TripService.GetTripsByUser(_UserA, _Guest);
        }

        [Test]
        public void NotReturnAnyTripIfUsersAreNotFriends()
        {
            var friend = UserBuilder.CreateUser()
                .WithFriends(new[] {_UserA})
                .WithTrips(new[] {_ToLondon, _ToNewYork})
                .Build();
            IList<Trip> tripList = _TripService.GetTripsByUser(friend, _RegisteredUser);
            Assert.That(tripList.Count, Is.EqualTo(0));
        }

        [Test]
        public void ReturnTripsIfUsersAreFriend()
        {
            var trips = new List<Trip>{_ToLondon, _ToNewYork};
            var friend =
                UserBuilder.CreateUser()
                .WithFriends(new[] { _RegisteredUser })
                .WithTrips(trips).Build();

            _TripDao.Setup(a => a.TripsByUser(friend)).Returns(trips);
            var tripList = _TripService.GetTripsByUser(friend, _RegisteredUser);
            
            Assert.That(tripList.Count, Is.EqualTo(2));
        }

    }
}