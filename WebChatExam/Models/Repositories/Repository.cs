using Microsoft.EntityFrameworkCore;
using System;
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
        public static UserModel SystemUser { get; set; }
        public static int CurrentChatId { get; set; } = -1;

        public static void EraseData()
        {
            Chats.Clear();
            Messages.Clear();
            CurrentChatId = -1; 
        }

        public static void UpdateChats(ApplicationContext context, UserModel user)
        {
            Chats = context.Chats.Where(x => x.Users.Contains(user)).Include(x => x.Users).Include(x => x.Messages).ToList();
            Chats.Sort(delegate(ChatModel a, ChatModel b) 
                {
                    if (a.Messages.LastOrDefault() == null || b.Messages.LastOrDefault() == null) return 0;
                    else return b.Messages.Last().Time.CompareTo(a.Messages.Last().Time);
                });
        }

        public static List<UserModel> GetUsersInCurrentChat()
        {
            return Chats.Where(x => x.Id == CurrentChatId).FirstOrDefault()?.Users;
        }
    }
}
