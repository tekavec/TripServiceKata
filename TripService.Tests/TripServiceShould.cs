using System.Collections.Generic;
using NUnit.Framework;

namespace TripService.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private static User _LoggedUser;
        private static User _RegisteredUser = new User();
        private static User _Guest = null;
        private static Trip _ToLondon = new Trip();
        private static Trip _ToNewYork = new Trip();
        private static User _UserA = new User();
        private TestableTripService _TripService;

        [SetUp]
        public void Init()
        {
            _TripService = new TestableTripService();
            _LoggedUser = _RegisteredUser;
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
            var friend = UserBuilder.CreateUser()
                .WithFriends(new[] {_UserA})
                .WithTrips(new[] {_ToLondon, _ToNewYork})
                .Build();
            IList<Trip> tripList = _TripService.GetTripsByUser(friend);
            Assert.That(tripList.Count, Is.EqualTo(0));
        }

        [Test]
        public void ReturnTripsIfUsersAreFriend()
        {
            var friend =
                UserBuilder.CreateUser()
                .WithFriends(new[] {_LoggedUser})
                .WithTrips(new []{_ToLondon, _ToNewYork}).Build();
            
            var tripList = _TripService.GetTripsByUser(friend);
            
            Assert.That(tripList.Count, Is.EqualTo(2));
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