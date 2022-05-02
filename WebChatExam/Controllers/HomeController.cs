using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebChatExam.Models;

namespace WebChatExam.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Chats()
        {
            return View();
        }

        public IActionResult Authorization()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ChatsSearch()
        {
            return View("Settings");
        }

        [HttpPost]
        public IActionResult SendMessage()
        {
            return View("Settings");
        }

        [HttpPost]
        public IActionResult OpenChat()
        {
            return View("Settings");
        }

        [HttpPost]
        public IActionResult UpdateUserLoginPassword(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel();
                user.Login = model.Login;
                user.PasswodHash = CalculateHash(model.Password).ToString();
                user.PhotoUrl = CurrentUser.PhotoUrl;
                user.Id= CurrentUser.Id;
                _context.Users.Update(user);
                _context.SaveChanges();
                CurrentUser.EditUser(user);
                return View("Settings");
            }
            return View("Settings");
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
