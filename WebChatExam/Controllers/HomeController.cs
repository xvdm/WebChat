using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebChatExam.Models;
using WebChatExam.Models.Chats;
using Microsoft.EntityFrameworkCore;
using WebChatExam.Models.Repositories;

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
            var chats = _context.Users.Include(c => c.Chats).Where(x => x.Id == CurrentUser.Id).FirstOrDefault().Chats;
            Repository.Chats = chats;

            if(chats.Count > 0)
                OpenChat(chats.Where(x=> x.Id == Repository.CurrentChatId).FirstOrDefault());

            if (CurrentUser.Id == 0) return RedirectToAction("Login", "Authorization");
            else return View();
        }

        public IActionResult Settings()
        {
            if (CurrentUser.Id == 0) return RedirectToAction("Login", "Authorization");
            else return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            CurrentUser.EraseData();
            Repository.EraseData();
            return RedirectToAction("Login", "Authorization");
        }

        [HttpPost]
        public IActionResult ChatsSearch()
        {
            return View("Settings");
        }

        [HttpPost]
        public IActionResult AddUserToChat(string login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login);
            if (user is not null)
            {
                var chat = _context.Chats.FirstOrDefault(x => x.Id == Repository.CurrentChatId);
                user.Chats.Add(chat);
                chat.Users.Add(user);
                _context.SaveChanges();
            }
            else
            {
                var error = new ErrorViewModel();
                return View("Error", error);
            }
            return RedirectToAction("Chats");
        }

        [HttpPost]
        public IActionResult CreateChat(string name)
        {
            if(name != null && name.Length > 0 && name.Trim().Length > 0)
            {
                UserModel user = _context.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);

                ChatModel chat = new ChatModel();
                chat.Name = name;
                chat.PhotoUrl = "~/images/default-chat.png";

                user.Chats.Add(chat);
                chat.Users.Add(user);

                _context.Chats.Add(chat);
                _context.SaveChanges();
                return RedirectToAction("Chats");
            }
            else
            {
                var error = new ErrorViewModel();
                return View("Error", error);
            }
        }

        [HttpPost]
        public IActionResult SendMessage(string text)
        {
            var message = new MessageModel();
            message.Text = text;
            message.Time = DateTime.Now;
            message.Sender = _context.Users.FirstOrDefault(x=> x.Id == CurrentUser.Id);
            message.Chat = _context.Chats.FirstOrDefault(x => x.Id == Repository.CurrentChatId);

            _context.Messages.Add(message);

            _context.SaveChanges();

            return RedirectToAction("Chats");
        }

        [HttpPost]
        public IActionResult OpenChat(ChatModel chat)
        {
            if (chat != null)
            {
                Repository.Messages.Clear();
                var messages = _context.Messages.Include(m => m.Chat).Include(s => s.Sender).Where(x => x.Chat.Id == chat.Id).ToList();
                Repository.CurrentChatId = chat.Id;
                Repository.Messages = messages;
            }
            return RedirectToAction("Chats");
        }

        [HttpPost]
        public IActionResult UpdateUserLoginPassword(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // логин - уникальный
                if (_context.Users.Where(x => x.Login == model.Login).Any())
                {
                    var error = new ErrorViewModel();
                    return View("Error", error);
                }
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

        //protected void Upload(object sender, EventArgs e)
        //{
        //    //Access the File using the Name of HTML INPUT File.
        //    HttpPostedFileBase postedFile = Request.Files["FileUpload"];

        //    //Check if File is available.
        //    if (postedFile != null && postedFile.ContentLength > 0)
        //    {
        //        //Save the File.
        //        string filePath = Microsoft.AspNetCore.Server.MapPath("~/images/") + Path.GetFileName(postedFile.FileName);
        //        postedFile.SaveAs(filePath);
        //    }
        //}
    }
}
