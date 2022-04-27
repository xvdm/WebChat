using Microsoft.AspNetCore.Mvc;
using System;
using WebChatExam.Models;

namespace WebChatExam.Controllers
{
    public class AuthorizationController : Controller
    {
        private ApplicationContext _context;

        public AuthorizationController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            var registerModel = new RegisterModel();
            return View(registerModel);
        }
        public IActionResult Login()
        {
            var loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
        public IActionResult RegisterAction(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel();
                user.Login = registerModel.Login;
                user.PasswodHash = CalculateHash(registerModel.Password).ToString();
                user.PhotoUrl = "~/images/default-user.png";
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login", "Authorization");
            }
            else
            {
                var error = new ErrorViewModel();
                return View("Error", error);
            }
        }

        [HttpPost]
        public IActionResult LoginAction(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Home", "Chats");
            }
            else
            {
                var error = new ErrorViewModel();
                return View("Error", error);
            }
        }

        private static UInt64 CalculateHash(string read)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }
    }
}
