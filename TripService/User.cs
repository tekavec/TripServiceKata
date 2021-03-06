﻿using System.Collections.Generic;
using System.Linq;

namespace TripService
{
    public class User
    {
        private readonly IList<User> _Friends = new List<User>();
        private readonly List<Trip> _Trips = new List<Trip>();

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

        public List<Trip> Trips()
        {
            return _Trips;
        }

        public bool IsFriendWith(User user)
        {
            return Enumerable.Contains(_Friends, user);
        }
    }
}