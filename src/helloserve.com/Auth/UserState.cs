namespace helloserve.com.Auth
{
    public class UserState
    {
        public bool IsLoggedIn { get; set; }
        public string DisplayName { get; set; }
        public string PictureUrl { get; set; }

        public static UserState LoggedOutState = new UserState { IsLoggedIn = false };
    }
}
