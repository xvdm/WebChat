namespace WebChatExam.Models.Chats
{
    public class ChatUnreadMessagesModel
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int UnreadMessages { get; set; }
    }
}
