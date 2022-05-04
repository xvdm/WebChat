using System.Collections.Generic;

namespace WebChatExam.Models.Chats
{
    public class ChatModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; } = "~/images/default-chat.png";
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
        public List<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
