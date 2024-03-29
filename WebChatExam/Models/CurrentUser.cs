﻿namespace WebChatExam.Models
{
    public static class CurrentUser
    {
        private static int _id;
        private static string _login;
        private static string _passwordHash;
        private static string _photoUrl;
        private static string _email;

        public static void EditUser(UserModel user)
        {
            if (user != null)
            {
                _id = user.Id;
                _login = user.Login;
                _passwordHash = user.PasswodHash;
                _photoUrl = user.PhotoUrl;
                _email = user.Email;
            }
        }

        public static void UpdatePhoto(string photoPath)
        {
            if(photoPath != null) _photoUrl = photoPath;
        }

        public static void EraseData()
        {
            _id = 0;
            _login = null;
            _passwordHash = null;
            _photoUrl = null;
            _email = null;
        }

        public static int Id { get { return _id; } }
        public static string Login { get { return _login; } }
        public static string PasswordHash { get { return _passwordHash; } }
        public static string PhotoUrl { get { return _photoUrl; } }
        public static string Email { get { return _email; } }
    }
}
