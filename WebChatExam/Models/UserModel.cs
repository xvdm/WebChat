using System.Collections.Generic;
using WebChatExam.Models.Chats;

namespace WebChatExam.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswodHash { get; set; }
        public string PhotoUrl { get; set; }
        public List<ChatModel> Chats { get; set; } = new List<ChatModel>();
    }
}
