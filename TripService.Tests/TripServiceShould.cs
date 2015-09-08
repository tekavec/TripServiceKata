using System.Collections.Generic;
using NUnit.Framework;

namespace TripService.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private static User _LoggedUser;
        private readonly User _RegisteredUser = new User();
        private readonly User _Guest = null;
        private readonly User _UserA = new User();

        [Test]
        [ExpectedException(typeof (UserNotLoggedInException))]
        public void ThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            _LoggedUser = _Guest;
            var tripService = new TestableTripService();

            tripService.GetTripsByUser(_Guest);
        }

        [Test]
        public void NotReturnAnyTripIfUsersAreNotFriends()
        {
            _LoggedUser = _RegisteredUser;
            var tripService = new TestableTripService();
            var tripOne = new Trip();
            _UserA.AddTrip(tripOne);

            var friend  = new User();
            friend.AddFriend(_UserA);

            IList<Trip> tripList = tripService.GetTripsByUser(friend);
            Assert.That(tripList.Count, Is.EqualTo(0));
        }

        private class TestableTripService : TripService
        {
            protected override User GetLoggedUser()
            {
                return _LoggedUser;
            }
        }
    }

}