using System.Collections.Generic;

namespace TripService
{
    public class User
    {
        private readonly IList<User> _Friends = new List<User>();
        private readonly IList<Trip> _Trips = new List<Trip>();

        public IEnumerable<User> GetFriends()
        {
            return _Friends;
        }

        public void AddFriend(User user)
        {
            _Friends.Add(user);
        }

        public void AddTrip(Trip trip)
        {
            _Trips.Add(trip);
        }
    }
}