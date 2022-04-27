using Microsoft.AspNetCore.Mvc;
using System;
using WebChatExam.Models;

namespace WebChatExam.Controllers
{
    public class AuthorizationController : Controller
    {
        //private ApplicationContext _context;

        //public AuthorizationController(ApplicationContext context)
        //{
        //    _context = context;
        //}

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
            return RedirectToAction("Login", "Authorization");
        }

        [HttpPost]
        public IActionResult LoginAction(LoginModel loginModel)
        {
            return RedirectToAction("Home", "Chats");
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
