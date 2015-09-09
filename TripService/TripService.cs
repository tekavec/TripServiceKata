using System.Collections.Generic;

namespace TripService
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            List<Trip> tripList = new List<Trip>();
            User loggedUser = GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }
                if (isFriend)
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