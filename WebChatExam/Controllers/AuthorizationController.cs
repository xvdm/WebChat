using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebChatExam.Models;
using WebChatExam.Models.Chats;
using WebChatExam.Models.Repositories;

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
                // логин - уникальный
                if(_context.Users.Where(x => x.Login == registerModel.Login).Any())
                {
                    var error = new ErrorViewModel();
                    return View("Error", error);
                }
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
                var user = _context.Users.FirstOrDefault(x => x.Login == loginModel.Login && x.PasswodHash == CalculateHash(loginModel.Password).ToString());
                if (user == null)
                {
                    var error = new ErrorViewModel();
                    return View("Error", error);
                }
                CurrentUser.EditUser(user);
                return RedirectToAction("Chats", "Home");
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
