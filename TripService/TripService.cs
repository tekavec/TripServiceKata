using System.Collections.Generic;

namespace TripService
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            User loggedUser = GetLoggedUser();
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }
            return user.IsFriendWith(loggedUser)
                ? TripsBy(user) 
                : NoTrips();
        }

        private static List<Trip> NoTrips()
        {
            return new List<Trip>();
        }

        protected virtual List<Trip> TripsBy(User user)
        {
            return TripDao.FindTripsByUser(user);
        }

        protected virtual User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}