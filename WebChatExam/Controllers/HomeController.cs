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
        IWebHostEnvironment _appEnvironment;

        public HomeController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            Repository.SystemUser = _context.Users.FirstOrDefault(x=>x.Login == "SYSTEMLOGIN");
        }


        [HttpGet]
        public IActionResult UpdateChatsList()
        {
            var user = _context.Users.Where(x => x.Id == CurrentUser.Id).FirstOrDefault();
            Repository.UpdateChats(_context, user);
            return PartialView("ChatPartials/PartialChatsList");
        }

        [HttpGet]
        public IActionResult UpdateMessages()
        {
            var messages = _context.Messages.Include(m => m.Chat).Include(x => x.Chat.Users).Where(x => x.Chat.Id == Repository.CurrentChatId).ToList();
            Repository.Messages = messages;
            return PartialView("ChatPartials/PartialMessages");
        }

        [HttpGet]
        public IActionResult UpdateChatSettings()
        {
            return PartialView("ChatPartials/PartialChatSettings");
        }

        [HttpGet] 
        public IActionResult Chats()
        {
            var user = _context.Users.Where(x => x.Id == CurrentUser.Id).FirstOrDefault();
            Repository.UpdateChats(_context, user);

            if (CurrentUser.Id == 0) return RedirectToAction("Login", "Authorization");
            else return PartialView();
        }

        [HttpGet]
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
        public IActionResult CreateChat(string name)
        {
            if(name != null && name.Length > 0 && name.Trim().Length > 0)
            {
                UserModel user = _context.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);

                ChatModel chat = new ChatModel();
                chat.Name = name;
                chat.PhotoUrl = "~/images/default-chat.png";

                Repository.SystemUser.Chats.Add(chat);
                _context.Users.FirstOrDefault(x => x.Login == "SYSTEMLOGIN")?.Chats.Add(chat);
                chat.Users.Add(Repository.SystemUser);

                user.Chats.Add(chat);
                chat.Users.Add(user);

                _context.Chats.Add(chat);
                _context.SaveChanges();
                Repository.Chats.Add(chat);

            }
            return PartialView("ChatPartials/PartialChatsList");
        }

        [HttpPost]
        public IActionResult SendMessage(string text)
        {
            if (text != null && text.Length > 0 && text.Trim().Length > 0)
            {
                UserModel currentUser = _context.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);

                var message = new MessageModel();
                message.Text = text;
                message.Time = DateTime.Now;
                message.Sender = currentUser;
                message.Chat = _context.Chats.FirstOrDefault(x => x.Id == Repository.CurrentChatId);

                _context.Messages.Add(message);
                _context.SaveChanges();
                Repository.Messages.Add(message);

                Repository.UpdateChats(_context, currentUser);
            }
            return PartialView("ChatPartials/PartialMessages");
        }

        [HttpPost]
        public IActionResult OpenChat(string id)
        {
            int chatId = int.Parse(id);
            Repository.Messages.Clear();
            var messages = _context.Messages.Include(m => m.Chat).Include(x => x.Chat.Users).Where(x => x.Chat.Id == chatId).ToList();
            Repository.CurrentChatId = chatId;
            Repository.Messages = messages;

            return PartialView("ChatPartials/PartialMessages");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(IFormFile uploadFile, LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(uploadFile != null)
                {
                    string filetype = uploadFile.FileName.Substring(uploadFile.FileName.LastIndexOf('.') + 1);
                    string path = $"/images/{CurrentUser.Id}.{filetype}";
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadFile.CopyToAsync(fileStream);
                    }
                    CurrentUser.UpdatePhoto(path);
                    _context.Users.Where(x => x.Id == CurrentUser.Id).FirstOrDefault().PhotoUrl = path;
                    _context.SaveChanges();
                } 
                if (model.Login != CurrentUser.Login)
                {
                    // логин - уникальный
                    if (_context.Users.Where(x => x.Login == model.Login).Any())
                    {
                        var error = new ErrorViewModel();
                        //return View("Error", error);
                    }
                    UserModel user = new UserModel();
                    user.Login = model.Login;
                    user.PasswodHash = CalculateHash(model.Password).ToString();
                    user.PhotoUrl = CurrentUser.PhotoUrl;
                    user.Id = CurrentUser.Id;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    CurrentUser.EditUser(user);
                }
            }
            return RedirectToAction("Chats");
        }

        [HttpPost]
        public void AddUserToChat(string login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login);
            if (user is not null)
            {
                bool contains = false;
                foreach(var x in Repository.GetUsersInCurrentChat())
                {
                    if(x.Id == user.Id)
                    {
                        contains = true;
                        break;
                    }
                }
                if (contains == false)
                {
                    var chat = _context.Chats.FirstOrDefault(x => x.Id == Repository.CurrentChatId);
                    user.Chats.Add(chat);
                    chat.Users.Add(user);

                    chat.Messages.Add(new MessageModel() { Sender = Repository.SystemUser, Text = $"{CurrentUser.Login} adds {user.Login} to chat", Time = DateTime.Now });

                    _context.SaveChanges();

                    UserModel currentUser = _context.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);
                    Repository.UpdateChats(_context, currentUser);

                    Repository.Messages.Clear();
                    var messages = _context.Messages.Include(m => m.Chat).Include(x => x.Chat.Users).Where(x => x.Chat.Id == Repository.CurrentChatId).ToList();
                    Repository.Messages = messages;
                }
            }
        }

        [HttpPost]
        public void LeaveChat(string captcha)
        {
            if(Repository.CurrentChatId >= 0 && captcha == "LEAVE")
            {
                ChatModel chat = _context.Chats.Where(x=> x.Id == Repository.CurrentChatId).Include(c=>c.Users).FirstOrDefault();
                UserModel user = _context.Users.FirstOrDefault(x => x.Id == CurrentUser.Id);

                chat.Messages.Add(new MessageModel() { Sender = Repository.SystemUser, Text = $"{user.Login} left chat", Time = DateTime.Now});
                chat.Users.Remove(user);
                _context.SaveChanges();

                Repository.CurrentChatId = -1;
            }
        }

        [HttpPost] 
        public async Task<IActionResult> UpdateChatPhoto(IFormFile uploadFile)
        {
            if (uploadFile != null)
            {
                string filetype = uploadFile.FileName.Substring(uploadFile.FileName.LastIndexOf('.') + 1);
                string path = $"/images/{Repository.CurrentChatId}.{filetype}";
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fileStream);
                }
                _context.Chats.FirstOrDefault(x => x.Id == Repository.CurrentChatId).PhotoUrl = path;
                _context.SaveChanges();
            }
            return View("Chats");
        }


        private static ulong CalculateHash(string read)
        {
            ulong hashedValue = 3074457345618258791ul;
            for (int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }
    }
}

