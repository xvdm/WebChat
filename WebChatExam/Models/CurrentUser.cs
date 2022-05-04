namespace WebChatExam.Models
{
    public static class CurrentUser
    {
        private static int _id;
        private static string _login;
        private static string _passwordHash;
        private static string _photoUrl;

        public static void EditUser(UserModel user)
        {
            if (user != null)
            {
                _id = user.Id;
                _login = user.Login;
                _passwordHash = user.PasswodHash;
                _photoUrl = user.PhotoUrl;
            }
        }

        public static void EraseData()
        {
            _id = 0;
            _login = null;
            _passwordHash = null;
            _photoUrl=null;
        }

        public static int Id { get { return _id; } }
        public static string Login { get { return _login; } }
        public static string PasswordHash { get { return _passwordHash; } }
        public static string PhotoUrl { get { return _photoUrl; } }
    }
}
