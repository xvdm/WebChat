using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebChatExam.Models.Repositories
{
    public static class Repository
    {
        private static readonly ApplicationContext _context;

        class ChatComparer : IEqualityComparer<ChatModel>
        {
            public bool Equals(ChatModel x, ChatModel y)
            {
                if(x.Id == y.Id) return true;
                else return false;
            }

            public int GetHashCode([DisallowNull] ChatModel obj)
            {
                return obj.GetHashCode();
            }
        }

        public static IEnumerable<ChatModel> GetChatsForUser(int userId)
        {
            //var chatsForUser = _context.UsersInChat.Where(x => x.UserId == userId).ToList();
            //var result = _context.Chats.Where(x => chatsForUser.Contains(x.Id));
            var chatsForUser = _context.UsersInChat.Where(x => x.UserId == userId).ToList();
            //IEnumerable<ChatModel> result = (from cu in chatsForUser
            //                                 join c in _context.Chats on cu.Id
            //                                 equals c
            //                                 select cu);
            //return result;
        }
    }
}
