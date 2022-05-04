using System;

namespace WebChatExam.Models.Chats
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public UserModel Sender { get; set; }
        public ChatModel Chat { get; set; }
    }
}
