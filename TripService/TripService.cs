using System.Collections.Generic;

namespace TripService
{
    public class TripService
    {
        private readonly TripDao _TripDao = new TripDao();

        public TripService(TripDao tripDao)
        {
            _TripDao = tripDao;
        }

        public virtual List<Trip> GetFriendTrips(User friend, User loggedInUser)
        {
            ValidateUser(loggedInUser);
            return friend.IsFriendWith(loggedInUser)
                ? TripsBy(friend) 
                : NoTrips();
        }

        private static void ValidateUser(User loggedInUser)
        {
            if (loggedInUser == null)
            {
                throw new UserNotLoggedInException();
            }
        }

        private static List<Trip> NoTrips()
        {
            return new List<Trip>();
        }

        protected virtual List<Trip> TripsBy(User user)
        {
            return _TripDao.TripsByUser(user);
        }

    }
}