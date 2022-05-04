using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WebChatExam.Models.Chats;

namespace WebChatExam.Models.Repositories
{
    public static class Repository
    {

        public static List<ChatModel> Chats { get; set; } = new List<ChatModel>();
        public static List<MessageModel> Messages { get; set; } = new List<MessageModel>();
        public static int CurrentChatId { get; set; }
        

        public static IEnumerable<ChatModel> GetChatsForUser(int userId)
        {
            //var chatsForUser = _context.UsersInChat.Where(x => x.UserId == userId).ToList();
            //var result = _context.Chats.Where(x => chatsForUser.Contains(x.Id));
            //var chatsForUser = _context.UsersInChat.Where(x => x.UserId == userId).ToList();
            //IEnumerable<ChatModel> result = (from cu in chatsForUser
            //                                 join c in _context.Chats on cu.Id
            //                                 equals c
            //                                 select cu);

            //using (var context = new ApplicationContext())
            //{
            //var chats = _context.Chats.Include(x => x.Users.Where(u => u.Id == CurrentUser.Id)).ToList();

            //}

            return null;
        }
    }
}
