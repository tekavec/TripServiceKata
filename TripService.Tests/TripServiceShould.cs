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
        private TestableTripService _TripService;

        [SetUp]
        public void Init()
        {
            _TripService = new TestableTripService();
        }

        [Test]
        [ExpectedException(typeof (UserNotLoggedInException))]
        public void ThrowAnExceptionWhenUserIsNotLoggedIn()
        {
            _LoggedUser = _Guest;

            _TripService.GetTripsByUser(_Guest);
        }

        [Test]
        public void NotReturnAnyTripIfUsersAreNotFriends()
        {
            _LoggedUser = _RegisteredUser;
            var trip = new Trip();
            _UserA.AddTrip(trip);

            var friend  = new User();
            friend.AddFriend(_UserA);

            IList<Trip> tripList = _TripService.GetTripsByUser(friend);
            Assert.That(tripList.Count, Is.EqualTo(0));
        }

        [Test]
        public void ReturnTripsIfUsersAreFriend()
        {
            _LoggedUser = _RegisteredUser;
            var trip = new Trip();
            var friend = new User();
            _LoggedUser.AddTrip(trip);
            friend.AddFriend(_LoggedUser);
            
            var tripList = _TripService.GetTripsByUser(friend);
            
            Assert.That(tripList.Count, Is.EqualTo(1));
        }

        private class TestableTripService : TripService
        {
            protected override User GetLoggedUser()
            {
                return _LoggedUser;
            }

            protected override List<Trip> TripsBy(User user)
            {
                var trips = new List<Trip>();
                trips.AddRange(user.Trips());
                foreach (var friend in user.GetFriends())
                {
                    trips.AddRange(friend.Trips());
                }
                return trips;
            }
        }
    }

}