using System.Collections.Generic;

namespace TripService.Tests
{
    public class UserBuilder
    {
        private static IList<User> _Friends = new List<User>();
        private IList<Trip> _Trips = new List<Trip>();

        public static UserBuilder CreateUser()
        {
            return new UserBuilder();
        }


        public UserBuilder WithFriends(IList<User> friends)
        {
            _Friends = friends;
            return this;
        }

        public UserBuilder WithTrips(IList<Trip> trips)
        {
            _Trips = trips;
            return this;
        }

        public User Build()
        {
            var user = new User();
            AddFriendsTo(user);
            AddTripsTo(user);
            return user;
        }

        private void AddTripsTo(User user)
        {
            foreach (var trip in _Trips)
            {
                user.AddTrip(trip);
            }
        }

        private void AddFriendsTo(User user)
        {
            foreach (var friend in _Friends)
            {
                user.AddFriend(friend);
            }
        }
    }
}