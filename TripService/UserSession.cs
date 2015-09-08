namespace TripService
{
    public class UserSession
    {
        private static UserSession _Instance;

        public static UserSession GetInstance()
        {
            return _Instance ?? (_Instance = new UserSession());
        }

        public User GetLoggedUser()
        {
            throw new System.NotImplementedException();
        }
    }
}