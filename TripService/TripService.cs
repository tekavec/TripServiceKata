using System.Collections.Generic;

namespace TripService
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            List<Trip> tripList = new List<Trip>();
            User loggedUser = GetLoggedUser();
            if (loggedUser != null)
            {
                if (user.IsFriendWith(loggedUser))
                {
                    tripList = TripsBy(user);
                }
                return tripList;
            }
            throw new UserNotLoggedInException();
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