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

        public virtual List<Trip> GetTripsByUser(User user, User loggedInUser)
        {
            if (loggedInUser == null)
            {
                throw new UserNotLoggedInException();
            }
            return user.IsFriendWith(loggedInUser)
                ? TripsBy(user) 
                : NoTrips();
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