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
        public static int CurrentChatId { get; set; } = -1;

        public static void EraseData()
        {
            Chats.Clear();
            Messages.Clear();
            CurrentChatId = -1;
        }
    }
}
