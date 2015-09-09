using System.Collections.Generic;

namespace TripService
{
    public class TripDao
    {
        public static List<Trip> FindTripsByUser(User user)
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